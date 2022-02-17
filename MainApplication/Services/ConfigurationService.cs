﻿using MainApplication.Objects;
using MainApplication.Objects.Enums;
using MainApplication.Storages;

namespace MainApplication.Services;

internal sealed class ConfigurationService
{
    private static readonly ConfigurationService Instance = new();

    private string? _configPath;

    private AStorage<Config>? _storage;

    public Config Config;

    public ConfigurationService()
    {
        Config = new Config("en-US", SaveFileType.JSON, new List<string>(), new List<string>());
        LoadConfigFile();
    }

    private void LoadConfigFile()
    {
        _configPath = AppDomain.CurrentDomain.BaseDirectory + @"data\config.json";
        _storage = new JsonStorage<Config>(_configPath);
        Directory.CreateDirectory(Path.GetDirectoryName(_configPath) ?? string.Empty);
        if (!File.Exists(_configPath))
            File.CreateText(_configPath).Close();
        var config = _storage.GetElement();
        if (config == default)
            SaveCurrentConfig();
        else
            Config = config;
    }

    public void SaveCurrentConfig()
    {
        if (Config != null) _storage?.WriteElement(Config);
    }

    internal void ChangeSaveFileType(SaveFileType saveFileType)
    {
        Config.SaveFileType = saveFileType;
        SaveCurrentConfig();
    }


    public static ConfigurationService GetInstance()
    {
        return Instance;
    }

    public bool AlreadyEncryptExtensionWithSameName(string extensionName)
    {
        return Config.EncryptExtensions.Contains(extensionName);
    }

    public bool AlreadyPriorityFileWithSameName(string fileName)
    {
        return Config.PriorityFiles.Contains(fileName);
    }


    public bool AddEncryptExtension(string extension)
    {
        if (AlreadyEncryptExtensionWithSameName(extension))
            return false;
        Config.EncryptExtensions.Add(extension);
        SaveCurrentConfig();
        return true;
    }

    public bool AddPriorityFile(string file)
    {
        if (AlreadyPriorityFileWithSameName(file))
            return false;
        Config.PriorityFiles.Add(file);
        SaveCurrentConfig();
        return true;
    }


    public bool RemoveEncryptExtension(string extension)
    {
        Config.EncryptExtensions.Remove(extension);
        SaveCurrentConfig();
        return true;
    }

    public bool RemovePriorityFile(string file)
    {
        Config.PriorityFiles.Remove(file);
        SaveCurrentConfig();
        return true;
    }
}