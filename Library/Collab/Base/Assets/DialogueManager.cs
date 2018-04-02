using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

	public Text dialogueText;
	public Text nameText;

	private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		Debug.Log ("Started Dialogue");

		sentences = new Queue<string>();
	}

	public void DisplayDialogue(Dialogue dialogue) {
		// TODO method needs to show full sentence once before skipping to next 
		sentences.Clear();

		foreach(string sentence in dialogue.sentences) {
			sentences.Enqueue(sentence);
		}
		nameText.text = dialogue.name;
		DisplayNextSentence();
	}

	public void DisplayNextSentence() {
		if (sentences.Count == 0) {
			EndDialog ();
			return;
		}
		dialogueText.text = sentences.Dequeue ();
	}


	public void EndDialog() {
		Debug.Log ("Ended Dialogue");
		return;
	}

//	IEnumerator
}
