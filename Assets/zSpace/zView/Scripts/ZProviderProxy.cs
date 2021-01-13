using System;
using System.Reflection;
using UnityEngine;

namespace zSpace.zView
{
    public class ZProviderProxy
    {
        public static ZProviderProxy Instance
        {
            get
            {
                if(ZProviderProxy._instance == null)
                {
                    ZProviderProxy._instance = new ZProviderProxy();
                }

                return ZProviderProxy._instance;
            }
        }

        public Vector2 DisplaySize
        {
            get
            {
                if(this._displaySize == null)
                {
                    return Vector2.zero;
                }

                return (Vector2)this._displaySize.GetValue(this._proxyType);
            }
        }

        private ZProviderProxy()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            if(assembly != null)
            {
                this._proxyType = assembly.GetType("zSpace.Core.ZProvider");
                if(this._proxyType != null)
                {
                    this._displaySize =
                        this._proxyType.GetProperty( "DisplaySize",
                        BindingFlags.Static | BindingFlags.Public);

                    if(this._displaySize == null)
                    {
                        Debug.LogError("Failed to find the" +
                            "zSpace.Core.ZProvider.DisplaySize accessor");
                    }
                }
                else
                {
                    Debug.LogError("Failed to find the zSpace.Core.ZProvider" +
                        " type in executing assembly");
                }
            }
            else
            {
                Debug.LogError(
                    "Failed to get executing assembly for ZProviderProxy.");
            }
        }   

        private static ZProviderProxy _instance = null;
        private Type _proxyType = null;
        private PropertyInfo _displaySize = null;
    }
}
