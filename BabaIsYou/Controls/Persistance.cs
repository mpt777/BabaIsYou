using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;

namespace BabaIsYou.Controls
{
    public class Persistance
    {
        public bool saving = false;
        public bool loading = false;
        public void SaveGameState(InputMap inputMap)
        {
            lock (this)
            {
                if (!saving)
                {
                    saving = true;
                    //
                    // Create something to save
                    FinalizeSaveAsync(inputMap);
                }
            }
        }

        private async void FinalizeSaveAsync(InputMap states)
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile("InputMap.json", FileMode.Create))
                        {
                            if (fs != null)
                            {
                                DataContractJsonSerializer mySerializer = new DataContractJsonSerializer(typeof(InputMap));
                                mySerializer.WriteObject(fs, states);
                            }
                        }
                    }
                    catch (IsolatedStorageException)
                    {
                        // Ideally show something to the user, but this is demo code :)
                    }
                }

                saving = false;
            });
        }

        /// <summary>
        /// Demonstrates how to deserialize an object from storage device
        /// </summary>
        public void LoadGameState()
        {
            lock (this)
            {
                if (!loading)
                {
                    loading = true;
                    FinalizeLoadAsync();
                }
            }
        }
        public InputMap inputMap = new InputMap();

        private async void FinalizeLoadAsync()
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        if (storage.FileExists("InputMap.json"))
                        {
                            using (IsolatedStorageFileStream fs = storage.OpenFile("InputMap.json", FileMode.Open))
                            {
                                if (fs != null)
                                {
                                    DataContractJsonSerializer mySerializer = new DataContractJsonSerializer(typeof(InputMap));
                                    inputMap = (InputMap)mySerializer.ReadObject(fs);
                                }
                            }
                        }
                    }
                    catch (IsolatedStorageException)
                    {
                        // Ideally show something to the user, but this is demo code :)
                    }
                }

                loading = false;
            });
        }
    }
    
}

