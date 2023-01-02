using UnityEngine;

namespace GameManagers
{
    public class InterfaceManager : BaseGameManager
    {
        [SerializeField]
        private MemoryCard originalCard;
    
        [SerializeField]
        private Sprite[] images;
        
        private const int GridRows = 2;
        private const int GridCols = 4;
        private const float OffsetX = 2f;
        private const float OffsetY = 2.5f;

        public override void Startup()
        {
            Vector3 startPos = originalCard.transform.position;

            int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3};
            numbers = ShuffleArray(numbers);

            for (int i = 0; i < GridCols; i++) {
                for (int j = 0; j < GridRows; j++) {
                    MemoryCard card;
                    if (i == 0 && j == 0) {
                        card = originalCard;
                    } else {
                        card = Instantiate(originalCard);
                    }

                    int index = j * GridCols + i;
                    int imageId = numbers[index];
                    card.SetCard(imageId, images[imageId]);

                    float posX = OffsetX * i + startPos.x;
                    float posY = -(OffsetY * j) + startPos.y;
                    card.transform.position = new Vector3(posX, posY, startPos.z);
                }
            }
        }
        
        private int[] ShuffleArray(int[] numbers) {
            int[] newArray = numbers.Clone() as int[];
        
            for (int i = 0; i < newArray.Length; i++) {
                int tmp = newArray[i];
                int r = Random.Range(i, newArray.Length);
                newArray[i] = newArray[r];
                newArray[r] = tmp;
            }

            return newArray;
        }
    }
}
