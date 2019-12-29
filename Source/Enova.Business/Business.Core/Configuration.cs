using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.EntityClient;
using System.Configuration;
using Enova.Business.Old.Types;

namespace Enova.Business.Old.Core
{
    public class Configuration
    {
        private static System.Configuration.Configuration config = null;
        public static System.Configuration.Configuration Config
        {
            get
            {
                if (config == null)
                    config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                return config;
            }
        }

        public static ConfigurationSectionGroup DataBaseSettingsGroup = null;
        public static List<DataBaseSettings> DataBaseSettingsList = null;

        private static ConnectionStringSettingsCollection connectionStrings = null;
        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                if (connectionStrings == null)
                    ReadConnectionStrings();
                return connectionStrings;
            }
        }

        private static void createDataBaseSetingsGroup()
        {
            DataBaseSettingsGroup = new ConfigurationSectionGroup();
            config.SectionGroups.Add("DataBaseSetings", DataBaseSettingsGroup);
            
            if (ConnectionStrings.Count > 0)
            {
                bool first = true;
                foreach (ConnectionStringSettings conn in ConnectionStrings)
                {
                    if (conn.ProviderName == "System.Data.EntityClient")
                    {
                        DataBaseSection secion = new DataBaseSection();
                        secion.Settings = new DataBaseSetttingsElement(first);
                        if (first)
                            first = false;
                        DataBaseSettingsGroup.Sections.Add(conn.Name, secion);
                    }
                }
            }
            
            config.Save(ConfigurationSaveMode.Modified);
        }

        public static void LoadConfiguration()
        {
            if (config == null)
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            DataBaseSettingsGroup = config.GetSectionGroup("DataBaseSetings");

            if (DataBaseSettingsGroup == null)
            {
                createDataBaseSetingsGroup();
            }

            DataBaseSettingsList = GetDataBaseSettings();
        }

        public static void SaveConfiguration()
        {
            config.Save(ConfigurationSaveMode.Modified);
            
        }

        public static void ReadConnectionStrings()
        {
            connectionStrings = Config.ConnectionStrings.ConnectionStrings;
        }

        public static ConnectionStringSettings GetConnectionStringSettings(string name)
        {
            foreach (ConnectionStringSettings conn in ConnectionStrings)
            {
                if (conn.Name == name)
                    return conn;
            }
            return null;
        }



        public static List<DataBaseSettings> GetDataBaseSettings()
        {
            List<DataBaseSettings> dataBases = new List<DataBaseSettings>();

            if (ConnectionStrings.Count > 0)
            {
                foreach (ConnectionStringSettings conn in ConnectionStrings)
                {
                    if (conn.ProviderName == "System.Data.EntityClient")
                    {

                        EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder(conn.ConnectionString);
                        SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder(entityBuilder.ProviderConnectionString);

                        DataBaseSection databaseSection = (DataBaseSection)DataBaseSettingsGroup.Sections[conn.Name];

                        dataBases.Add(new DataBaseSettings()
                        {
                            Name = conn.Name,
                            OrginalName = conn.Name,
                            Server = sqlBuilder.DataSource,
                            Catalog = sqlBuilder.InitialCatalog,
                            IntegratedSecurity = sqlBuilder.IntegratedSecurity,
                            UserId = sqlBuilder.UserID,
                            Password = sqlBuilder.Password,
                            ConnectionString = conn.ConnectionString,
                            ProviderConnectionString = entityBuilder.ProviderConnectionString,
                            Default = databaseSection == null ? false : databaseSection.Settings.Default,
                            IsNew = false
                        });
                    }

                }
            }

            return dataBases;
        }

        public static DataBaseSettings GetDataBaseSettings(string name)
        {
            foreach (var db in DataBaseSettingsList)
            {
                if (db.Name == name)
                    return db;
            }
            return null;
        }

        public static DataBaseSettings GetDefaultDataBaseSettings()
        {
            if (DataBaseSettingsList != null)
            {
                foreach (var db in DataBaseSettingsList)
                {
                    if (db.Default) return db;
                }
            }
            return null;
        }

        private static void insertConnectionString(string name, string connectionString, string providerName)
        {
            ConnectionStringSettings conn = new ConnectionStringSettings(name, connectionString, providerName);
            config.ConnectionStrings.ConnectionStrings.Add(conn);
        }

        public static void UpdateDataBaseSettings(DataBaseSettings dataBase)
        {
            if (dataBase.IsNew)
            {
                insertConnectionString(dataBase.Name, dataBase.ConnectionString, "System.Data.EntityClient");
            }
            else
            {
                ConnectionStringSettings conn = GetConnectionStringSettings(dataBase.OrginalName);
                if (conn != null)
                {
                    conn.Name = dataBase.Name;
                    conn.ConnectionString = dataBase.ConnectionString;
                }
            }

            SaveConfiguration();
            DataBaseSettingsList.Clear();
            DataBaseSettingsList = GetDataBaseSettings();
        }

        public static void DeleteDataBaseSettings(DataBaseSettings dataBase)
        {
            ConnectionStringSettings conn = GetConnectionStringSettings(dataBase.Name);
            if (conn != null)
            {
                config.ConnectionStrings.ConnectionStrings.Remove(conn);
                DataBaseSettingsGroup.Sections.Remove(dataBase.Name);
                SaveConfiguration();
                DataBaseSettingsList.Clear();
                DataBaseSettingsList = GetDataBaseSettings();
            }
        }

        public static string GetSetting(string name)
        {
            KeyValueConfigurationElement element = Config.AppSettings.Settings[name];
            if (element != null)
                return element.Value;
            return null;
        }

        public static bool GetBoolSetting(string name)
        {
            string s = GetSetting(name);
            if (!string.IsNullOrEmpty(s))
            {
                bool b;
                if (bool.TryParse(s, out b))
                    return b;
            }
            return false;
        }

        public static int? GetIntSettings(string name)
        {
            string s = GetSetting(name);
            if (!string.IsNullOrEmpty(s))
            {
                int i;
                if (int.TryParse(s, out i))
                    return i;
            }
            return null;
        }

        public static void SetSetting(string name, string value)
        {
            KeyValueConfigurationElement element = Config.AppSettings.Settings[name];
            if (element == null)
            {
                element = new KeyValueConfigurationElement(name, value);
                Config.AppSettings.Settings.Add(element);
            }
            else
            {
                element.Value = value;
            }


        }


    }
}
