using UnityEngine;
using System.Collections.Generic;

namespace Assignment
{
    public class Assignment : MonoBehaviour
    {
        public void Start()
        {
            AS01_CountWords();
            AS02_CountNumber();
            AS03_CheckValidBrackets();
            AS04_PrintReverseLinkedList();
            AS05_FindMiddleElement();
            AS06_MergeDictionaries();
            AS07_RemoveDuplicatesFromLinkedList();
            AS08_TopFrequentNumber();
            AS09_PlayerInventory();
            AS10_GameEventQueue();
            AS11_PlayerStatsTracker();
        }

        #region Assignment

        [Header("AS01 - Count Words")]
        [SerializeField] private string[] as01Words;

        public void AS01_CountWords()
        {
            if (as01Words == null) return;

            Dictionary<string, int> wordCounts = new Dictionary<string, int>();

            foreach (string word in as01Words)
            {
                if (wordCounts.ContainsKey(word))
                {
                    wordCounts[word]++;
                }
                else
                {
                    wordCounts[word] = 1;
                }
            }

            foreach (KeyValuePair<string, int> pair in wordCounts)
            {
                Debug.Log($"word: '{pair.Key}' count: {pair.Value}");
            }
        }

        [Header("AS02 - Count Number")]
        [SerializeField] private int[] as02Numbers;

        public void AS02_CountNumber()
        {
            if (as02Numbers == null) return;

            Dictionary<int, int> numberCounts = new Dictionary<int, int>();

            foreach (int num in as02Numbers)
            {
                if (numberCounts.ContainsKey(num))
                {
                    numberCounts[num]++;
                }
                else
                {
                    numberCounts[num] = 1;
                }
            }

            foreach (KeyValuePair<int, int> pair in numberCounts)
            {
                Debug.Log($"number: {pair.Key} count: {pair.Value}");
            }
        }

        [Header("AS03 - Check Valid Brackets")]
        [SerializeField] private string as03Input;

        public void AS03_CheckValidBrackets()
        {
            string input = as03Input ?? "";
            
            Dictionary<char, char> bracketPairs = new Dictionary<char, char>
            {
                { ')', '(' },
                { ']', '[' },
                { '}', '{' }
            };

            Stack<char> stack = new Stack<char>();
            bool isValid = true;

            foreach (char c in input)
            {
                if (c == '(' || c == '[' || c == '{')
                {
                    stack.Push(c);
                }
                else if (bracketPairs.ContainsKey(c))
                {
                    if (stack.Count == 0 || stack.Peek() != bracketPairs[c])
                    {
                        isValid = false;
                        break;
                    }
                    stack.Pop();
                }
            }

            if (stack.Count > 0)
            {
                isValid = false;
            }

            Debug.Log(isValid ? "Valid" : "Invalid");
        }

        [Header("AS04 - Print Reverse Linked List")]
        [SerializeField] private IntLinkedListInput as04List = new IntLinkedListInput();

        public void AS04_PrintReverseLinkedList()
        {
            LinkedList<int> list = as04List.GetLinkedList();

            if (list == null || list.Count == 0)
            {
                Debug.Log("List is empty");
                return;
            }

            LinkedListNode<int> current = list.Last;
            while (current != null)
            {
                Debug.Log(current.Value);
                current = current.Previous;
            }
        }

        [Header("AS05 - Find Middle Element")]
        [SerializeField] private StringLinkedListInput as05List = new StringLinkedListInput();

        public void AS05_FindMiddleElement()
        {
            LinkedList<string> list = as05List.GetLinkedList();

            if (list == null || list.Count == 0)
            {
                Debug.Log("List is empty");
                return;
            }

            LinkedListNode<string> slow = list.First;
            LinkedListNode<string> fast = list.First;

            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }

            Debug.Log(slow.Value);
        }

        [Header("AS06 - Merge Dictionaries")]
        [SerializeField] private StringIntDictionaryInput as06FirstDictionary = new StringIntDictionaryInput();
        [SerializeField] private StringIntDictionaryInput as06SecondDictionary = new StringIntDictionaryInput();

        public void AS06_MergeDictionaries()
        {
            Dictionary<string, int> dict1 = as06FirstDictionary.GetDictionary();
            Dictionary<string, int> dict2 = as06SecondDictionary.GetDictionary();

            Dictionary<string, int> mergedDictionary = new Dictionary<string, int>(dict1);

            foreach (KeyValuePair<string, int> pair in dict2)
            {
                if (mergedDictionary.ContainsKey(pair.Key))
                {
                    mergedDictionary[pair.Key] += pair.Value;
                }
                else
                {
                    mergedDictionary[pair.Key] = pair.Value;
                }
            }

            foreach (KeyValuePair<string, int> pair in mergedDictionary)
            {
                Debug.Log($"key: {pair.Key}, value: {pair.Value}");
            }
        }

        [Header("AS07 - Remove Duplicates From Linked List")]
        [SerializeField] private IntLinkedListInput as07List = new IntLinkedListInput();

        public void AS07_RemoveDuplicatesFromLinkedList()
        {
            LinkedList<int> list = as07List.GetLinkedList();

            if (list == null || list.Count <= 1)
            {
                if (list != null)
                {
                    foreach (int val in list)
                    {
                        Debug.Log(val);
                    }
                }
                return;
            }

            Dictionary<int, bool> seen = new Dictionary<int, bool>();
            LinkedListNode<int> current = list.First;

            while (current != null)
            {
                LinkedListNode<int> next = current.Next;

                if (seen.ContainsKey(current.Value))
                {
                    list.Remove(current);
                }
                else
                {
                    seen[current.Value] = true;
                }

                current = next;
            }

            foreach (int val in list)
            {
                Debug.Log(val);
            }
        }

        [Header("AS08 - Top Frequent Number")]
        [SerializeField] private int[] as08Numbers;

        public void AS08_TopFrequentNumber()
        {
            if (as08Numbers == null || as08Numbers.Length == 0)
            {
                Debug.Log("Input array is empty");
                return;
            }

            Dictionary<int, int> counts = new Dictionary<int, int>();
            foreach (int num in as08Numbers)
            {
                if (counts.ContainsKey(num))
                {
                    counts[num]++;
                }
                else
                {
                    counts[num] = 1;
                }
            }

            int topNumber = as08Numbers[0];
            int maxCount = counts[topNumber];

            foreach (KeyValuePair<int, int> pair in counts)
            {
                if (pair.Value > maxCount)
                {
                    maxCount = pair.Value;
                    topNumber = pair.Key;
                }
            }

            Debug.Log($"{topNumber} count: {maxCount}");
        }

        [Header("AS09 - Player Inventory")]
        [SerializeField] private StringIntDictionaryInput as09Inventory = new StringIntDictionaryInput();
        [SerializeField] private string as09ItemName;
        [SerializeField] private int as09Quantity;

        public void AS09_PlayerInventory()
        {
            Dictionary<string, int> inventory = as09Inventory.GetDictionary();

            if (inventory.ContainsKey(as09ItemName))
            {
                inventory[as09ItemName] += as09Quantity;
            }
            else
            {
                inventory[as09ItemName] = as09Quantity;
            }

            foreach (KeyValuePair<string, int> item in inventory)
            {
                Debug.Log($"{item.Key}: {item.Value}");
            }
        }

        [Header("AS10 - Game Event Queue")]
        [SerializeField] private GameEventLinkedListInput as10EventQueue = new GameEventLinkedListInput();

        public void AS10_GameEventQueue()
        {
            LinkedList<GameEvent> eventQueue = as10EventQueue.GetLinkedList();

            if (eventQueue == null || eventQueue.Count == 0)
            {
                Debug.Log("Event queue is empty");
                return;
            }

            while (eventQueue.Count > 0)
            {
                GameEvent currentEvent = eventQueue.First.Value;
                eventQueue.RemoveFirst();

                Debug.Log($"Processing event: {currentEvent.Name}");
                Debug.Log($"Remaining events in queue: {eventQueue.Count}");

                string typeStr = currentEvent.EventType != null ? currentEvent.EventType.ToLower() : "";
                
                switch (typeStr)
                {
                    case "enemy":
                        Debug.Log($"Enemy event processed - {currentEvent.Name}");
                        break;
                    case "powerup":
                        Debug.Log($"Power-up event processed - {currentEvent.Name}");
                        break;
                    case "level":
                        Debug.Log($"Level event processed - {currentEvent.Name}");
                        break;
                    default:
                        Debug.Log($"Unknown event processed - {currentEvent.Name}");
                        break;
                }
            }
        }

        [Header("AS11 - Player Stats Tracker")]
        [SerializeField] private StringIntDictionaryInput as11PlayerStats = new StringIntDictionaryInput();
        [SerializeField] private string as11StatName;
        [SerializeField] private int as11Value;

        public void AS11_PlayerStatsTracker()
        {
            Dictionary<string, int> playerStats = as11PlayerStats.GetDictionary();

            if (playerStats.ContainsKey(as11StatName))
            {
                playerStats[as11StatName] += as11Value;
            }
            else
            {
                playerStats[as11StatName] = as11Value;
            }

            Debug.Log($"Updated {as11StatName}: {playerStats[as11StatName]}");
            Debug.Log("Current player statistics:");

            foreach (KeyValuePair<string, int> stat in playerStats)
            {
                Debug.Log($"{stat.Key}: {stat.Value}");
            }
        }

        #endregion
    }
}