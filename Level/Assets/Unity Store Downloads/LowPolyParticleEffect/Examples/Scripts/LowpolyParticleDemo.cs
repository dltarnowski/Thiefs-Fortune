using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NeutronCat.LowpolyParticle.Example
{
    public class LowpolyParticleDemo : MonoBehaviour
    {
        public bool deactivateOtherGroup = true;
        List<Transform> _groupList = new List<Transform>();
        Transform _mainCam;
        Text _UIGroupName;
        int _index = 0, _indexPre = 0;

        void Awake()
        {
            var pg = GameObject.Find("ParticleGroups");
            foreach (var t in pg.GetComponentsInChildren<Transform>())
            {
                if (t.parent != null && t.parent.Equals(transform))
                    _groupList.Add(t);
            }

            _mainCam = Camera.main.transform;

            _UIGroupName = GameObject.Find("UIGroupName").GetComponent<Text>();

            SetGroup();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)) _index++;
            if (Input.GetKeyDown(KeyCode.LeftArrow)) _index--;

            if (_index < 0) _index = _groupList.Count - 1;
            else if (_index >= _groupList.Count) _index = 0;

            if (_index != _indexPre) SetGroup();
            _indexPre = _index;
        }

        void SetGroup()
        {
            var currentGroup = _groupList[_index];
            _mainCam.position = currentGroup.position;
            _mainCam.rotation = currentGroup.rotation;

            _UIGroupName.text = _groupList[_index].name;

            if (deactivateOtherGroup)
            {
                foreach (var g in _groupList)
                {
                    g.gameObject.SetActive(false);
                }
                currentGroup.gameObject.SetActive(true);
            }
        }
    }
}