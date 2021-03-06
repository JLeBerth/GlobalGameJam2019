﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnlocks : MonoBehaviour
{

    public class Node
    {
        public Node()
        {
            nextNodes = new List<Node>();
            nodeName = "Default Node";
            unlocked = false;
            cost = 0;
        }

        public Node(string nam, int cos, bool startUnlocked)
        {
            nextNodes = new List<Node>();
            nodeName = nam;
            cost = cos;
            unlocked = startUnlocked;
        }

        private List<Node> nextNodes;
        private Node previousNode;
        private bool unlocked;
        private int cost;
        private string nodeName;

        public List<Node> NextNodes { get { return NextNodes; } }
        public Node PreviousNode { get { return previousNode; } set { previousNode = value; } }
        public bool Unlocked { get { return unlocked; } }
        public string NodeName { get { return nodeName; } }

        /// <summary>
        /// Adds a node based on input values
        /// </summary>
        /// <param name="nam"></param>
        /// <param name="cos"></param>
        /// <param name="startUnlocked"></param>
        public void AddNode(string nam, int cos, bool startUnlocked)
        {
            int nodeIndex = nextNodes.Count;
            nextNodes.Add(new Node(nam, cos, startUnlocked));
            nextNodes[nodeIndex].PreviousNode = this;
        }

        /// <summary>
        /// Adds a node directly
        /// </summary>
        /// <param name=""></param>
        public void AddNode(Node toAdd)
        {
            int nodeIndex = nextNodes.Count;
            nextNodes.Add(toAdd);
            nextNodes[nodeIndex].PreviousNode = this;
        }

        /// <summary>
        /// Adds a node directly at idArray Position
        /// </summary>
        /// <param name="toAdd"></param>
        /// <param name="current"></param>
        /// <param name="idArray"></param>
        public void AddNode(Node toAdd,Node current , int[] idArray)
        {
            Node currentNode = current;
            for (int i = 0; i < idArray.Length; i++)
            {
                if (idArray[i] == -1)
                {
                    int nodeIndex = currentNode.NextNodes.Count;
                    currentNode.nextNodes.Add(toAdd);
                    currentNode.nextNodes[nodeIndex].previousNode = currentNode;
                }
                else
                {
                    currentNode = currentNode.nextNodes[idArray[i]];
                }
            }
        }

        /// <summary>
        /// Returns the node located at the ID arrays position
        /// </summary>
        /// <param name="idArray"></param>
        /// <returns></returns>
        public Node GetNode(int[] idArray, Node current)
        {
            Debug.Log("IS THIS WORKING?");
            
            Node currentNode = current;
            Debug.Log(currentNode);
            for(int i = 0; i < idArray.Length; i++)
            {
                if(idArray[i] == -1)
                {
                    Debug.Log(currentNode);
                    return currentNode;
                }
                else
                {
                    currentNode = currentNode.nextNodes[idArray[i]];
                    Debug.Log(currentNode);
                }
            }
            return null;
        }
        /// <summary>
        /// Unlock method checks cost ans if possible unlocks the node
        /// </summary>
        public bool Unlock()
        {
            if(previousNode.unlocked && Player.coal >= cost)
            {
                Player.coal -= cost;
                unlocked = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// unlocks node regardless of cost
        /// </summary>
        public void ForceUnlock()
        {
            if(previousNode.unlocked)
            {
                unlocked = true;
            }
        }
    }

    public static Node gunUnlocks;

    private void Start()
    {
        gunUnlocks = new Node("De Confianza", 0, true); //id -1
        gunUnlocks.AddNode("Hemingway", 100, false); // id 0,-1
        gunUnlocks.AddNode("Golden Ratio", 500, false); //id 1,-1
        gunUnlocks.AddNode("Pride and Accomplishment", 10000, false); // id 2,-1
    }

}
