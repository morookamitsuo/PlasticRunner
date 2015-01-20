using UnityEngine;
using System.Collections;

public class MapCreator : MonoBehaviour
{

		public static float BLOCK_WIDTH = 1.0f;
		// ブロックの幅
		public static float BLOCK_HEIGHT = 0.2f;
		// ブロックの高さ
		public static int BLOCK_NUM_IN_SCREEN = 24;
		// 画面内に収まるブロックの個数

		// ブロックに関する情報をまとめて管理するための構造体
		private struct FloorBlock
		{
				public bool is_created;
				// ブロックが作成済みか否か
				public Vector3 position;
				// ブロックの位置
		};

		private FloorBlock last_block;
		// 最後に作成したブロック
		private PlayerControl player = null;
		// シーン上のPlayerを保管
		private BlockCreator block_creator;
		// BlockCreatorを保管

	
		void Start ()
		{
				this.player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ();
				this.last_block.is_created = false;
				this.block_creator = this.gameObject.GetComponent<BlockCreator> ();
		}

	
		void Update ()
		{
				// プレイヤーのX位置を取得
				float block_generate_x = this.player.transform.position.x;
				// そこから、およそ半画面分、右へ移動
				// この位置が、ブロックを生み出す閾値になる
				block_generate_x += BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN + 1) / 2.0f;

				// 最後に作ったブロックの位置が閾値より小さい場合
				while (this.last_block.position.x < block_generate_x) {
						// ブロックを作る
						this.create_floor_block ();
				}
	
		}

		private void create_floor_block ()
		{
				Vector3 block_position; // これから作るブロックの位置

				if (!this.last_block.is_created) { // last_blockが未作成の場合
						// ブロックの位置を、とりあえずPlayerと同じにする
						block_position = this.player.transform.position;
						// それから、ブロックのX位置を半画面分、左に移動
						block_position.x -= BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
						// ブロックのY位置はゼロに
						block_position.y = 0.0f;
				} else { // last_blockが作成済みの場合
						// 今回作るブロックの位置を、前回作ったブロックと同じに
						block_position = this.last_block.position;
				}

				//ブロックを1ブロック分、右に移動
				block_position.x += BLOCK_WIDTH;

				// blockCreatorスクリプトのcreateBlock()メソッドに作成指示！
				// これまでのコードで設定したblock_positionを渡す
				this.block_creator.createBlock (block_position);

				//last_blockの位置を、今回の位置に更新。
				this.last_block.position = block_position;
				// ブロック作成済みなので、lastblockのiscreatedをtrueに。
				this.last_block.is_created = true;
		}

		public bool isDelete (GameObject block_object)
		{
				bool ret = false; // 戻り値

				// Player から、画面半分、左の位置
				// これが、消えるべきか田舎を決める閾値となる
				float left_limit = this.player.transform.position.x - BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);

				// ブロックの位置が閾値より小さい（左）なら、
				if (block_object.transform.position.x < left_limit) {
						ret = true; // 戻り値をtrue(消えてよし)に。
				}

				return(ret); // 判定結果を返す。
		}
}
