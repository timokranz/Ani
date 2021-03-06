﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelAllocator : MonoBehaviour {

    int passedAnimalID;
    GameObject model;
    AnimalDatabase database;
    Animal animal;

    public GameObject scene;
    public GameObject info1;
    public GameObject info2;
    public GameObject info3;
    public GameObject ARButton;
    public GameObject animalName;

    GameObject infoPanel;
    GameObject imagePanel;

    bool qualityMeterHidden = false;

    float[] part1_vector = new float[3];
    float[] part2_vector = new float[3];
    float[] part3_vector = new float[3];
    float[] part1_rotation = new float[3];
    float[] part2_rotation = new float[3];
    float[] part3_rotation = new float[3];

    Crosstales.RTVoice.Demo.AniVoice _scene;

    // Use this for initialization
    void Start () {
        database = GetComponent<AnimalDatabase>();
        // Get previous clicked animal ID
        animal = database.FetchAnimalByID(DataManager.animalClicked);
        _scene = scene.GetComponent<Crosstales.RTVoice.Demo.AniVoice>();

        // Instantiate model for previously clicked animal
        Debug.Log(animal.id);
        model = Instantiate((GameObject)Resources.Load("Prefab/Models" + animal.modelPath));
        model.transform.SetParent(GameObject.Find("UserDefinedTarget").transform);
        model.transform.localScale = new Vector3(animal.scale, animal.scale, animal.scale);
        model.transform.position = Vector3.zero;
        model.transform.Rotate(0, animal.rotation, 0);
        model.SetActive(false);
        model.name = "Model";

        /*

        // Instantiate Animation deleted after 2 seconds in the UDTEventHandler script
        GameObject mod = Instantiate((GameObject)Resources.Load("Prefab/Summon/SummonGround"));
        mod.transform.parent = model.transform;
        mod.transform.localScale = new Vector3(1F, 1F, 1F);

        */

        // Set Animal's name
        animalName.GetComponent<Text>().text = animal.name;

        // Vectors
        string[] splitted1 = animal.part1_vector.Split(',');
        string[] splitted2 = animal.part2_vector.Split(',');
        string[] splitted3 = animal.part3_vector.Split(',');
        // Rotations
        string[] splittedrot1 = animal.part1_rotation.Split(',');
        string[] splittedrot2 = animal.part2_rotation.Split(',');
        string[] splittedrot3 = animal.part3_rotation.Split(',');

        for (int i = 0; i < 3; i++)
        {
            part1_vector[i] = float.Parse(splitted1[i]);
            part2_vector[i] = float.Parse(splitted2[i]);
            part3_vector[i] = float.Parse(splitted3[i]);
            part1_rotation[i] = float.Parse(splittedrot1[i]);
            part2_rotation[i] = float.Parse(splittedrot2[i]);
            part3_rotation[i] = float.Parse(splittedrot3[i]);
        }
        
        

        // Set ScreenManager modified to false
        GameObject.Find("SceneManager").GetComponent<ScreenManager>().setModify(false);

        infoPanel = GameObject.Find("TargetBuilderCanvas").transform.GetChild(5).gameObject;
        imagePanel = GameObject.Find("TargetBuilderCanvas").transform.GetChild(6).gameObject;
    }

    public void speakIntro()
    {
        _scene.valueSpeak = animal.intro;
        _scene.Speak();
    }

    public void activateInfoSpeak()
    {
        // Add button informations for previously clicked animal

        info1.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/AnimalInfoButton/" + animal.name + "/1");
        info2.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/AnimalInfoButton/" + animal.name + "/2");
        info3.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/AnimalInfoButton/" + animal.name + "/3");

        Debug.Log("ANIMAL NAME " + animal.name);

        info1.GetComponent<Button>().onClick.AddListener(() => {
            // Enables Back to AR button
            ARButton.SetActive(true);
            // Sets Ani to speek info 1
            _scene.valueSpeak = animal.info1;
            _scene.Speak();
            GameObject.Find("ARCamera").GetComponent<Camera>().depth = 0;
            sync Synch = GameObject.Find("Synch").GetComponent<sync>();
            if (!Synch.isDuplicated())
            {
                Synch.duplicateModel();
                Synch.offSync();
            }
            // Zooms to Part 1
            GameObject.Find("Synch").GetComponent<ZoomParts>().showPart1(part1_vector,part1_rotation);

            if (!qualityMeterHidden)
            {
                GameObject.Find("QualityMeter").SetActive(false);
                qualityMeterHidden = true;
            }
            
            infoPanel.gameObject.SetActive(true);
            infoPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = animal.info1;

            imagePanel.SetActive(true);
            imagePanel.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/AnimalInfoImage/" + animal.name + "/1");
        });

       info2.GetComponent<Button>().onClick.AddListener(() => {
           // Enables Back to AR button
           ARButton.SetActive(true);
           // Sets Ani to speek info 1
           _scene.valueSpeak = animal.info2;
            _scene.Speak();
           GameObject.Find("ARCamera").GetComponent<Camera>().depth = 0;
           sync Synch = GameObject.Find("Synch").GetComponent<sync>();
           if (!Synch.isDuplicated())
           {
               Synch.duplicateModel();
               Synch.offSync();
           }
           // Zooms to Part 2
           GameObject.Find("Synch").GetComponent<ZoomParts>().showPart2(part2_vector, part2_rotation);
           GameObject.Find("TargetBuilderCanvas").transform.GetChild(6).gameObject.SetActive(true);

           if (!qualityMeterHidden)
           {
               GameObject.Find("QualityMeter").SetActive(false);
               qualityMeterHidden = true;
           }

           infoPanel.gameObject.SetActive(true);
           infoPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = animal.info2;

           imagePanel.SetActive(true);
           imagePanel.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/AnimalInfoImage/" + animal.name + "/2");
       });

       info3.GetComponent<Button>().onClick.AddListener(() => {
           // Enables Back to AR button
           ARButton.SetActive(true);
           // Sets Ani to speek info 1
           _scene.valueSpeak = animal.info3;
            _scene.Speak();
           GameObject.Find("ARCamera").GetComponent<Camera>().depth = 0;
           sync Synch = GameObject.Find("Synch").GetComponent<sync>();
           if (!Synch.isDuplicated())
           {
               Synch.duplicateModel();
               Synch.offSync();
           }
           // Zooms to Part 3
           GameObject.Find("Synch").GetComponent<ZoomParts>().showPart3(part3_vector, part3_rotation);
           GameObject.Find("TargetBuilderCanvas").transform.GetChild(6).gameObject.SetActive(true);

           if (!qualityMeterHidden)
           {
               GameObject.Find("QualityMeter").SetActive(false);
               qualityMeterHidden = true;
           }

           infoPanel.gameObject.SetActive(true);
           infoPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = animal.info3;

           imagePanel.SetActive(true);
           imagePanel.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/AnimalInfoImage/" + animal.name + "/3");

       });
    }

    public void hideSidePanels()
    {
        imagePanel.SetActive(false);
        infoPanel.SetActive(false);
    }

    public void enable()
    {
        model.SetActive(true);
    }

    public void disable()
    {
        StartCoroutine(disableIE());
    }

    public void enableInfo()
    {
        GameObject child;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Information");
        for(int i = 1; i < objects.Length; i++)
        {
            child = objects[i].transform.GetChild(0).gameObject;
            child.SetActive(true);
        }
        //GameObject.Find("Information").GetComponent<Renderer>().enabled = false;
    }

    public IEnumerator disableIE()
    {
        yield return new WaitForSeconds(0.0005f);
        model.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
