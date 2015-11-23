﻿using UnityEngine;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using Assets.Core.Configuration;
using System.Linq;
using System;
using Assets.Core.LevelSelector;
using Assets.Environment.Levers.LeverExample.Scripts;

public class DatabaseManager : MonoBehaviour, IDatabaseManager
{
    private Dictionary<int, Syllable> _alphabetDatabase;
    private Dictionary<int, Sign> _signsDatabase;
    private Dictionary<int, Sentence> _sentencesDatabase;

    private bool _alphabetDbLoaded;
    private bool _signsDbLoaded;
    private bool _sentencesDbLoaded;

    private WindowHandler _windowHandler;

    private

    void Awake()
    {

       // DontDestroyOnLoad(this.gameObject);
        _windowHandler = GameObject.FindGameObjectWithTag(Constants.Tags.WindowManager).GetComponent<WindowHandler>();

        try
        {
            LoadData();
        }
        catch (Exception)
        {
            _windowHandler.ActivateDialogWindow("Error", "Couldn't load database. Please reinstall the game from Play Store", false);
        }

    }

    void Start()
    {
        _windowHandler = GameObject.FindGameObjectWithTag(Constants.Tags.WindowManager).GetComponent<WindowHandler>();
    }


    // Adds the sign to the predefined id and name and sets it active
    public void AddSign(int id, List<int> syllableSequence)
    {
        if (_signsDatabase.ContainsKey(id))
        {
            Sign sign = _signsDatabase[id];
            sign.SyllableSequence = syllableSequence;
            sign.IsActive = true;
            _signsDatabase[id] = sign;
        }
    }

    //// Adds the sentence to the predefined id 
    //public void AddSentence(int id, List<int> signSequence)
    //{
    //    if (_sentencesDatabase.ContainsKey(id))
    //    {
    //        Sentence sentence = _sentencesDatabase[id];
    //        sentence.SignSequence = signSequence;
    //        _sentencesDatabase[id] = sentence;
    //    }
    //}


    // Returns the character of the database with the given ID
    public Syllable GetSyllable(int id)
    {
        if (_alphabetDatabase.ContainsKey(id))
        {
            return _alphabetDatabase[id];
        }

        return null;
    }

    // Returns the sign of the database with the given ID if it is active
    public Sign GetSign(int id)
    {
        if (_signsDatabase.ContainsKey(id) && _signsDatabase[id].IsActive)
        {
            return _signsDatabase[id];
        }
        return null;
    }

    //// Returns the sentence of the database with the given ID
    //public Sentence GetSentenceById(int id)
    //{
    //    if (_sentencesDatabase.ContainsKey(id))
    //    {
    //        return _sentencesDatabase[id];
    //    }
    //    return null;
    //}


    //// Returns the sentence of the database with the given ID
    //public int GetSentenceBySeq(List<int> signSequence)
    //{
    //    if (signSequence.Count > 0)
    //    {
    //        Sentence sentence =
    //            _sentencesDatabase.FirstOrDefault(x => x.Value.SignSequence.SequenceEqual(signSequence)).Value;

    //        return sentence != null ? sentence.id : -1;
    //    }
    //    return -1;
    //}

    public void SaveAllDb()
    {
        SaveSignsDb();
        SaveSentencesDb();
    }

    public void SaveSignsDb()
    {
        var signsPath = GetFilePath(Constants.XmlFiles.Signs);

        SignsContainer signsContainer = new SignsContainer();
        signsContainer.Signs = new List<Sign>(_signsDatabase.Values);

        var serializer = new XmlSerializer(typeof(SignsContainer));

        var stream = new MemoryStream();
        serializer.Serialize(stream, signsContainer);
        File.WriteAllBytes(signsPath, stream.ToArray());
        stream.Close();
    }

    public void SaveSentencesDb()
    {
        var sentencesPath = GetFilePath(Constants.XmlFiles.Sentences);

        SentencesContainer sentencesContainer = new SentencesContainer();
        sentencesContainer.Sentences = new List<Sentence>(_sentencesDatabase.Values);

        var serializer = new XmlSerializer(typeof(SentencesContainer));
        var stream = new FileStream(sentencesPath, FileMode.Create);
        serializer.Serialize(stream, sentencesContainer);
        stream.Close();
    }

    //Used to get an image from the resources folder in the game
    //It is used to get the default images
    public Sprite GetImage(string fileName)
    {
        return (Sprite)Resources.Load(fileName, typeof(Sprite));
    }

    private void LoadData()
    {
        LoadAlphabetDb();
        LoadsignsDb();
        LoadSentencesDb();
    }

    public bool DatabasesLoaded()
    {
        return (_alphabetDbLoaded && _signsDbLoaded && _sentencesDbLoaded);
    }

    public void ResetUserData()
    {
        // PlaterPrefs
        PlayerPrefsBool.SetBool("SideKickHat", false);
        PlayerPrefsBool.SetBool("PlayerHat", false);
        PlayerPrefs.SetInt("CurrencyAmount", CurrencyControl.currencyAmount * 0);

        PlayerPrefsBool.SetBool("SevenElever", false);
        LeverPulls.leverpulls = 0;

        Userlevels.GetInstance().ClearUserLevels();

        // XML's
        var signsPath = GetFilePath(Constants.XmlFiles.Signs);
        var bindata = (TextAsset)Resources.Load("Signs");
        File.WriteAllBytes(signsPath, bindata.bytes);
        signsPath = GetFilePath(Constants.XmlFiles.Sentences);
        bindata = (TextAsset)Resources.Load("Sentences");
        File.WriteAllBytes(signsPath, bindata.bytes);
        signsPath = GetFilePath(Constants.XmlFiles.Alphabet);
        bindata = (TextAsset)Resources.Load("Alphabet");
        File.WriteAllBytes(signsPath, bindata.bytes);

        LoadData();
    }

    private void LoadAlphabetDb()
    {
        var alphabetPath = GetFilePath(Constants.XmlFiles.Alphabet);
        if (!File.Exists(alphabetPath))
        {
            TextAsset bindata = Resources.Load("Alphabet") as TextAsset;
            if (bindata == null)
                _windowHandler.ActivateDialogWindow("Error", "Error while loading the alphabet database. Please reinstall the game from Play Store", false);
            else
                File.WriteAllBytes(alphabetPath, bindata.bytes);
        }

        var charSerializer = new XmlSerializer(typeof(AlphabetContainer));
        var bytes = File.ReadAllBytes(alphabetPath);
        var charStream = new MemoryStream(bytes);

        var container = charSerializer.Deserialize(charStream) as AlphabetContainer;

        charStream.Close();
        _alphabetDatabase = new Dictionary<int, Syllable>();

        if (container != null)
            foreach (Syllable syllable in container.Syllables)
            {
                _alphabetDatabase.Add(syllable.id, syllable);
            }
        _alphabetDbLoaded = true;
    }


    private void LoadsignsDb()
    {
        var signsPath = GetFilePath(Constants.XmlFiles.Signs);
        if (!File.Exists(signsPath))
        {
            TextAsset bindata = Resources.Load("Signs") as TextAsset;
            if (bindata == null)
                _windowHandler.ActivateDialogWindow("Error", "Error while loading the sign database. Please reinstall the game from Play Store", false);
            else
                File.WriteAllBytes(signsPath, bindata.bytes);
        }

        var charSerializer = new XmlSerializer(typeof(SignsContainer));
        var bytes = File.ReadAllBytes(signsPath);
        var charStream = new MemoryStream(bytes);
        var container = charSerializer.Deserialize(charStream) as SignsContainer;
        charStream.Close();

        _signsDatabase = new Dictionary<int, Sign>();
        if (container != null)
            foreach (Sign sign in container.Signs)
            {
                _signsDatabase.Add(sign.id, sign);
            }
        _signsDbLoaded = true;
    }

    private void LoadSentencesDb()
    {
        var sentencesPath = GetFilePath(Constants.XmlFiles.Sentences);
        if (!File.Exists(sentencesPath))
        {
            TextAsset bindata = Resources.Load("Sentences") as TextAsset;
            if (bindata == null)
                _windowHandler.ActivateDialogWindow("Error", "Error while loading the sentence database. Please reinstall the game from Play Store", false);
            else
                File.WriteAllBytes(sentencesPath, bindata.bytes);
        }


        var charSerializer = new XmlSerializer(typeof(SentencesContainer));
        var bytes = File.ReadAllBytes(sentencesPath);
        var charStream = new MemoryStream(bytes);
        var container = charSerializer.Deserialize(charStream) as SentencesContainer;
        charStream.Close();

        _sentencesDatabase = new Dictionary<int, Sentence>();
        if (container != null)
            foreach (Sentence sentence in container.Sentences)
            {
                _sentencesDatabase.Add(sentence.id, sentence);
            }

        _sentencesDbLoaded = true;
    }

    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
}

public class Syllable
{
    [XmlAttribute("id")]
    public int id;

    public string ImageName;

    public string SoundName;
}

[XmlRoot("AlphabetCollection")]
public class AlphabetContainer
{
    [XmlArray("Syllables")]
    [XmlArrayItem("Syllable")]
    public List<Syllable> Syllables = new List<Syllable>();
}

public class Sign
{
    [XmlAttribute("id")]
    public int id;

    public string Name;

    public List<int> SyllableSequence;

    public bool IsActive;
}

[XmlRoot("SignsCollection")]
public class SignsContainer
{
    [XmlArray("Signs")]
    [XmlArrayItem("Sign")]
    public List<Sign> Signs = new List<Sign>();
}


public class Sentence
{
    [XmlAttribute("id")]
    public int id;

    public List<int> SignSequence;
}

[XmlRoot("SentencesCollection")]
public class SentencesContainer
{
    [XmlArray("Sentences")]
    [XmlArrayItem("Sentence")]
    public List<Sentence> Sentences = new List<Sentence>();
}

