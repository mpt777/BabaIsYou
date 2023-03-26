using BabaIsYou.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Component = BabaIsYou.Components.Component;

namespace BabaIsYou.Entities
{
    /// <summary>
    /// A named entity that contains a collection of <see cref="Component"/> instances.
    /// <para>Instances of this class should be constructed using <see cref="ECSGame.AddEntity(string, Component[])"/>.</para>
    /// <para>This class cannot be inherited.
    /// Data should be added to custom entities using <see cref="Component"/> and <see cref="System"/> types.</para>
    /// <para>This class implements <see cref="IEnumerable{T}"/> of type <see cref="Component"/> to allow
    /// for easy iteration over its components.</para>
    /// </summary>
    public sealed class Entity
    {
        /// <summary>
        /// This entity's components.
        /// <para>This field is read-only. It should be added to using <see cref="Add(Component[])"/> and removed from
        /// using <see cref="Remove(Component)"/> or <see cref="Clear"/>, so this entity can update existing systems about changes made to it.</para>
        /// </summary>
        private readonly Dictionary<Type, Component> components = new Dictionary<Type, Component>();

        private static uint m_nextId = 0;
        /// <summary>
        /// Constructs a new <see cref="Entity"/> with the given name.
        /// <para>This should be unique within the game, so <see cref="ECSGame.AddEntity(string, Component[])"/>
        /// is the preferred way to construct entities.</para>
        /// </summary>
        public Entity()
        {
            Id = m_nextId++;
        }

        /// <summary>
        /// Gets the unique ID of this entity.
        /// </summary>
        public uint Id { get; private set; }

        /// <summary>
        /// Returns whether this Entity contains a component of the given type.
        /// </summary>
        /// <param name="type">A type assignable to <see cref="Component"/> that this entity should check for in its component map.</param>
        /// <returns>Returns true if this entity contains a component of the given type, otherwise returns false.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="type"/> is not assignable to <see cref="Component"/>.</exception>
        public bool ContainsComponent(Type type)
        {
            return components.ContainsKey(type) && components[type] != null;
        }

        /// <summary>
        /// Returns whether this Entity contains any component of the given type.
        /// </summary>
        public bool ContainsComponent<TComponent>()
            where TComponent : Component
        {
            return ContainsComponent(typeof(TComponent));
        }

        /// <summary>
        /// Attaches components to this entity
        /// </summary>
        /// <param name="newComponents">The components to be inserted.</param>
        public void Add(params Component[] newComponents)
        {
            Debug.Assert(components != null, "the list of components cannot be null");

            foreach (Component comp in newComponents)
            {
                Type type = comp.GetType();

                // Make sure the type is derived from Component.
                // This ensures that all components are passed by reference and it
                // encourages good ECS practice by making the code user derive from an empty
                // class with no behaviors.
                Debug.Assert(typeof(Component).IsAssignableFrom(type), string.Format("The given type should be assignable to {0}.", typeof(Component)));
                Debug.Assert(!this.components.ContainsKey(type), string.Format("A component of type {0} is already attached to this entity.", type));

                this.components.Add(type, comp);
            }
        }

        /// <summary>
        /// Allows a single component to be added
        /// </summary>
        /// <param name="component"></param>
        public void Add(Component component)
        {
            Debug.Assert(component != null, "component cannot be null");
            Debug.Assert(!this.components.ContainsKey(component.GetType()), "cannot add the same component twice");

            this.components.Add(component.GetType(), component);
        }

        /// <summary>
        /// Removes all components from this entity.
        /// </summary>
        public void Clear()
        {
            components.Clear();
        }

        /// <summary>
        /// Removes components from this entity
        /// </summary>
        public void Remove(params Component[] componentsToRemove)
        {
            foreach (Component component in componentsToRemove)
            {
                this.components.Remove(component.GetType());
            }
        }

        /// <summary>
        /// Allows a single component to be removed
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        public void Remove<TComponent>()
            where TComponent : Component
        {
            this.components.Remove(typeof(TComponent));
        }

        /// <summary>
        /// Returns the component in this entity that is of the given type,
        /// or throws a <see cref="ComponentNotFoundException"/> if no such component is found in this entity.
        /// </summary>        
        public TComponent GetComponent<TComponent>()
            where TComponent : Component
        {
            Debug.Assert(components.ContainsKey(typeof(TComponent)), string.Format("component of type {0} is not a part of this entity", typeof(TComponent)));
            return (TComponent)this.components[typeof(TComponent)];
        }

        public bool HasComponent<TComponent>()
            where TComponent : Component
        {
            return this.components.ContainsKey(typeof(TComponent));
        }

        /// <summary>
        /// Returns a human-friendly string representation of this entity, in the format of its name followed by a comma-separated
        /// list of its components' types.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}: {1}", Id, string.Join(", ", from c in components.Values select c.GetType().Name));
        }
    }
}