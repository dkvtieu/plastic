using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

	public Text dialogueText;
	public Text nameText;

	private string currentSentence;
	private IEnumerator currentSentenceCoroutine;
	private bool currentSentenceCoroutineRunning;
	private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		Debug.Log ("Started Dialogue");

		sentences = new Queue<string>();
	}

	// method to trigger beginning of dialogue
	public void StartDialogue(Dialogue dialogue) {
		ClearDialogue ();

		foreach(string sentence in dialogue.sentences) {
			sentences.Enqueue(sentence);
		}
		nameText.text = dialogue.name;
		DisplayNextSentence();
	}

	// method to continue the current dialogue's sentences
	public void DisplayNextSentence() {
		// if DisplayStaggeredSentenceText is not finished and DisplayNextSentence is triggered,
		// display full text before next 'continue' trigger
		if (currentSentenceCoroutineRunning) {
			StopCoroutine (currentSentenceCoroutine);
			currentSentenceCoroutineRunning = false;
			dialogueText.text = currentSentence;
			return;
		}

		if (sentences.Count == 0) {
			ClearDialogue ();
			return;
		}


		currentSentence = sentences.Dequeue ();
		currentSentenceCoroutine = DisplayStaggeredSentenceText (currentSentence, 0.1f);
		StartCoroutine(currentSentenceCoroutine);
	}

	// method to clear dialogue (either manually or reaching end of sentence queue in DisplayNextSentence)
	public void ClearDialogue() {
		Debug.Log ("Ended Dialogue");
		sentences.Clear ();
		dialogueText.text = null;
		nameText.text = null;
		return;
	}

	// staggers display text to mimic 'typing' effect with defined intervals between chars
	IEnumerator DisplayStaggeredSentenceText(string sentence, float intervalSpeed = -1f) {
		dialogueText.text = "";
		currentSentenceCoroutineRunning = true;
		foreach (char c in sentence) {
			dialogueText.text += c;

			// little hack so that if interval speed is not provided intervalSpeed is per frame
			if (intervalSpeed == -1f) {
				yield return null;
			} else {
				yield return new WaitForSeconds (intervalSpeed);
			}
		}
		currentSentenceCoroutineRunning = false;
	}
}
