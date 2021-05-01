using System;
using UnityEngine;

 public class TowerStats<T> : ScriptableObject
 {
     [SerializeField] private T _data;
     public T Data => _data;
 }
