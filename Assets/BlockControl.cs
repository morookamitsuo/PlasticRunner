using UnityEngine;
using System.Collections;

public class BlockControl : MonoBehaviour
{

		public MapCreator map_creator = null;
		//

		void Start ()
		{
				//MapCreator
				map_creator = GameObject.Find ("GameRoot").GetComponent<MapCreator> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (this.map_creator.isDelete (this.gameObject)) { // 見切れているなら
						GameObject.Destroy (this.gameObject); // 自分自身を削除。
				}
	
		}
}
