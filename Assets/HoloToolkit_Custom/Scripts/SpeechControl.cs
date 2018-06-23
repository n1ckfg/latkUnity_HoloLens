// https://developer.microsoft.com/en-us/windows/mixed-reality/voice_input_in_unity

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class SpeechControl : MonoBehaviour {

    public AudioSource audio;
    public LightningArtist latk;
    public LatkToGML latkToGml;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start () {
        //Create keywords for keyword recognizer
        keywords.Add("default", () => {
            // action to be performed when this keyword is spoken
            latk.mainColor = new Color(0.5f, 0.8f, 1f, 1f);
        });

        keywords.Add("blue", () => {
            latk.mainColor = new Color(0.1f, 0.2f, 1f, 1f);
        });

        keywords.Add("red", () => {
            latk.mainColor = new Color(1f, 0.1f, 0f, 1f);
        });

        keywords.Add("green", () => {
            latk.mainColor = new Color(0f, 1f, 0.1f, 1f);
        });

        keywords.Add("post", () => {
            latkToGml.doPost();
        });

        keywords.Add("undo", () => {
            latk.inputEraseLastStroke();
        });


        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args) {
        if (audio != null) audio.Play();
        System.Action keywordAction;
        // if the keyword recognized is in our dictionary, call that Action.
        if (keywords.TryGetValue(args.text, out keywordAction)) {
            keywordAction.Invoke();
        }
    }

}
