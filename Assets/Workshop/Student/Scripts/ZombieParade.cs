using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Solution
{
    public class ZombieParade : Character
    {
        // ใช้ LinkedList ในการจัดการส่วนของงูเพื่อประสิทธิภาพในการเพิ่ม/ลบ
        private LinkedList<GameObject> Parade = new LinkedList<GameObject>();

        public GameObject bodyPrefab; // Prefab ของส่วนลำตัวงู
        public float moveInterval = 0.5f; // ช่วงเวลาในการเคลื่อนที่

        private Vector3 moveDirection;
        private InputAction growAction;

        private void Start()
        {
            growAction = InputSystem.actions.FindAction("Grow");
            moveDirection = Vector3.up;
            isAlive = true;

            // 0. เพิ่มส่วนหัวเข้า LinkedList ก่อนเริ่มเดิน
            Parade.AddFirst(this.gameObject);

            // เริ่ม Coroutine สำหรับการเคลื่อนที่
            StartCoroutine(MoveParade());
        }

        private void Update()
        {
            if (growAction != null && growAction.triggered)
            {
                Grow();
            }
        }

        private Vector3 RandomizeDirection()
        {
            List<Vector3> possibleDirections = new List<Vector3>
            {
                Vector3.up,
                Vector3.down,
                Vector3.left,
                Vector3.right
            };

            return possibleDirections[Random.Range(0, possibleDirections.Count)];
        }

        // Coroutine สำหรับการเคลื่อนที่ทีละช่อง
        IEnumerator MoveParade()
        {
            while (isAlive)
            {
                // ถ้ามีแค่ส่วนหัวส่วนเดียว ให้ข้ามการขยับส่วนลำตัว
                if (Parade.Count <= 1)
                {
                    yield return new WaitForSeconds(moveInterval);
                    continue;
                }

                // 1. ดึงส่วนแรกของงูออกมา (ส่วนหัว)
                var firstNode = Parade.First;
                var firstGo = firstNode.Value;

                // 2. ดึงส่วนสุดท้ายของงูออกมา (หาง)
                var lastNode = Parade.Last;
                var lastGo = lastNode.Value;

                // 3. ลบส่วนสุดท้ายออกจาก LinkedList
                Parade.RemoveLast();

                // 5. สุ่มทิศทางและคำนวณพิกัดใหม่
                moveDirection = RandomizeDirection();
                int toX = (int)(firstGo.transform.position.x + moveDirection.x);
                int toY = (int)(firstGo.transform.position.y + moveDirection.y);

                while (IsCollision(toX, toY))
                {
                    moveDirection = RandomizeDirection();
                    toX = (int)(firstGo.transform.position.x + moveDirection.x);
                    toY = (int)(firstGo.transform.position.y + moveDirection.y);
                }

                // 6. เคลื่อนย้ายส่วนหางไปตำแหน่งใหม่
                positionX = toX;
                positionY = toY;
                lastGo.transform.position = new Vector3(positionX, positionY, 0);

                SpriteRenderer sr = lastGo.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    if (moveDirection == Vector3.right)
                    {
                        sr.flipX = true;
                    }
                    else if (moveDirection == Vector3.left)
                    {
                        sr.flipX = false;
                    }
                }

                // 7. นำส่วนที่เพิ่งย้ายไปแทรกต่อหลังส่วนหัว
                Parade.AddAfter(firstNode, lastNode);

                // รอตามเวลาที่กำหนด
                yield return new WaitForSeconds(moveInterval);
            }
        }

        private bool IsCollision(int x, int y)
        {
            // 4. ตรวจสอบสิ่งกีดขวาง
            return HasPlacement(x, y);
        }

        // ฟังก์ชันสำหรับเพิ่มส่วนของงู (Grow)
        private void Grow()
        {
            GameObject newPart = Instantiate(bodyPrefab);

            // กำหนดตำแหน่งเริ่มต้นให้อยู่ที่เดียวกับส่วนสุดท้ายของงู
            GameObject lastPart = Parade.Last.Value;
            newPart.transform.position = lastPart.transform.position;

            // เพิ่มส่วนใหม่เข้าไปท้ายสุดของ Linked List
            Parade.AddLast(newPart);
        }
    }
}