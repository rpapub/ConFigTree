using System;
using System.Data;
using System.Collections.Generic;

namespace Cpmf.Config
{
    public class CodedConfig
    {
        public SettingsConfig Settings { get; set; } = new();
        public ConstantsConfig Constants { get; set; } = new();
        public AssetsConfig Assets { get; set; } = new();
        public EndpointsConfig Endpoints { get; set; } = new();
        public override string ToString() =>
            $"CodedConfig {{ Settings={Settings}, Constants={Constants}, Assets={Assets}, Endpoints={Endpoints} }}";

        public static CodedConfig Load(Dictionary<string, DataTable> tables)
        {
            var cfg = new CodedConfig();
            if (tables.TryGetValue("Settings", out var t_Settings)) cfg.Settings = SettingsConfig.FromDataTable(t_Settings);
            if (tables.TryGetValue("Constants", out var t_Constants)) cfg.Constants = ConstantsConfig.FromDataTable(t_Constants);
            if (tables.TryGetValue("Assets", out var t_Assets)) cfg.Assets = AssetsConfig.FromDataTable(t_Assets);
            if (tables.TryGetValue("Endpoints", out var t_Endpoints)) cfg.Endpoints = EndpointsConfig.FromDataTable(t_Endpoints);
            return cfg;
        }
    }

    public class SettingsConfig
    {
        public string OrchestratorQueueName { get; set; } = "";
        public string OrchestratorFolderPath { get; set; } = "";
        public int MaxItemsPerRun { get; set; }
        public int RetryCount { get; set; }
        public string LogPrefix { get; set; } = "";

        public static SettingsConfig FromDataTable(DataTable dt)
        {
            var cfg = new SettingsConfig();
            foreach (DataRow row in dt.Rows)
            {
                var key   = row[0]?.ToString()?.Trim();
                var value = row[1]?.ToString()?.Trim() ?? "";
                switch (key)
                {
                    case "OrchestratorQueueName": cfg.OrchestratorQueueName = value; break;
                    case "OrchestratorFolderPath": cfg.OrchestratorFolderPath = value; break;
                    case "MaxItemsPerRun":
                        if (int.TryParse(value, out var v_MaxItemsPerRun)) cfg.MaxItemsPerRun = v_MaxItemsPerRun;
                        break;
                    case "RetryCount":
                        if (int.TryParse(value, out var v_RetryCount)) cfg.RetryCount = v_RetryCount;
                        break;
                    case "LogPrefix": cfg.LogPrefix = value; break;
                }
            }
            return cfg;
        }

        public override string ToString() =>
            $"SettingsConfig {{ OrchestratorQueueName={OrchestratorQueueName}, OrchestratorFolderPath={OrchestratorFolderPath}, MaxItemsPerRun={MaxItemsPerRun}, RetryCount={RetryCount}, LogPrefix={LogPrefix} }}";
    }

    public class ConstantsConfig
    {
        public int MaxRetryNumber { get; set; }
        public int MaxConsecutiveSystemExceptions { get; set; }
        public int RetryNumberGetTransactionItem { get; set; }

        public static ConstantsConfig FromDataTable(DataTable dt)
        {
            var cfg = new ConstantsConfig();
            foreach (DataRow row in dt.Rows)
            {
                var key   = row[0]?.ToString()?.Trim();
                var value = row[1]?.ToString()?.Trim() ?? "";
                switch (key)
                {
                    case "MaxRetryNumber":
                        if (int.TryParse(value, out var v_MaxRetryNumber)) cfg.MaxRetryNumber = v_MaxRetryNumber;
                        break;
                    case "MaxConsecutiveSystemExceptions":
                        if (int.TryParse(value, out var v_MaxConsecutiveSystemExceptions)) cfg.MaxConsecutiveSystemExceptions = v_MaxConsecutiveSystemExceptions;
                        break;
                    case "RetryNumberGetTransactionItem":
                        if (int.TryParse(value, out var v_RetryNumberGetTransactionItem)) cfg.RetryNumberGetTransactionItem = v_RetryNumberGetTransactionItem;
                        break;
                }
            }
            return cfg;
        }

        public override string ToString() =>
            $"ConstantsConfig {{ MaxRetryNumber={MaxRetryNumber}, MaxConsecutiveSystemExceptions={MaxConsecutiveSystemExceptions}, RetryNumberGetTransactionItem={RetryNumberGetTransactionItem} }}";
    }

    public class AssetsConfig
    {
        public OrchestratorAsset<string> QueueName { get; set; } = new();
        public OrchestratorAsset<int> MaxItemsPerRun { get; set; } = new();
        public OrchestratorAsset<bool> StrictMode { get; set; } = new();
        public OrchestratorAsset<string> ApiEndpoint { get; set; } = new();
        public OrchestratorAsset<object> GenericValue { get; set; } = new();

        public static AssetsConfig FromDataTable(DataTable dt)
        {
            var cfg = new AssetsConfig();
            foreach (DataRow row in dt.Rows)
            {
                var key   = row[0]?.ToString()?.Trim();
                var value = row[1]?.ToString()?.Trim() ?? "";
                switch (key)
                {
                    case "QueueName":
                        cfg.QueueName.AssetName = row[1]?.ToString()?.Trim() ?? "";
                        cfg.QueueName.Folder    = row[2]?.ToString()?.Trim() ?? "";
                        break;
                    case "MaxItemsPerRun":
                        cfg.MaxItemsPerRun.AssetName = row[1]?.ToString()?.Trim() ?? "";
                        cfg.MaxItemsPerRun.Folder    = row[2]?.ToString()?.Trim() ?? "";
                        break;
                    case "StrictMode":
                        cfg.StrictMode.AssetName = row[1]?.ToString()?.Trim() ?? "";
                        cfg.StrictMode.Folder    = row[2]?.ToString()?.Trim() ?? "";
                        break;
                    case "ApiEndpoint":
                        cfg.ApiEndpoint.AssetName = row[1]?.ToString()?.Trim() ?? "";
                        cfg.ApiEndpoint.Folder    = row[2]?.ToString()?.Trim() ?? "";
                        break;
                    case "GenericValue":
                        cfg.GenericValue.AssetName = row[1]?.ToString()?.Trim() ?? "";
                        cfg.GenericValue.Folder    = row[2]?.ToString()?.Trim() ?? "";
                        break;
                }
            }
            return cfg;
        }

        public override string ToString() =>
            $"AssetsConfig {{ QueueName={QueueName}, MaxItemsPerRun={MaxItemsPerRun}, StrictMode={StrictMode}, ApiEndpoint={ApiEndpoint}, GenericValue={GenericValue} }}";
    }

    public class EndpointsConfig
    {
        public OrchestratorAsset<object> BaseUrl { get; set; } = new();
        public OrchestratorAsset<object> OrchestratorFolder { get; set; } = new();

        public static EndpointsConfig FromDataTable(DataTable dt)
        {
            var cfg = new EndpointsConfig();
            foreach (DataRow row in dt.Rows)
            {
                var key   = row[0]?.ToString()?.Trim();
                var value = row[1]?.ToString()?.Trim() ?? "";
                switch (key)
                {
                    case "BaseUrl":
                        cfg.BaseUrl.AssetName = row[1]?.ToString()?.Trim() ?? "";
                        cfg.BaseUrl.Folder    = row[2]?.ToString()?.Trim() ?? "";
                        break;
                    case "OrchestratorFolder":
                        cfg.OrchestratorFolder.AssetName = row[1]?.ToString()?.Trim() ?? "";
                        cfg.OrchestratorFolder.Folder    = row[2]?.ToString()?.Trim() ?? "";
                        break;
                }
            }
            return cfg;
        }

        public override string ToString() =>
            $"EndpointsConfig {{ BaseUrl={BaseUrl}, OrchestratorFolder={OrchestratorFolder} }}";
    }

    public class OrchestratorAsset<T>
    {
        public string AssetName { get; set; } = "";
        public string Folder { get; set; } = "";
        public T Value { get; set; }
    }
}