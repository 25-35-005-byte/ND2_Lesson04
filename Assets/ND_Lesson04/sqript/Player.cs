using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float moveSpeed = 50.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.wKey.IsPressed())
        {
            transform.Translate(0, 0, moveSpeed * Time.deltaTime);
        }

        RaycastHit raycast;                 //光線の衝突情報
        //ベクトルを可視化する
        //原点から現在位置のベクトル
        CreateAndShowOfRay(Vector3.zero,transform.position,out raycast,Color.red);

        //移動ベクトル
        Vector3 moveVector = transform.position + new Vector3(0, 0, moveSpeed * Time.deltaTime);
        CreateAndShowOfRay(transform.position,moveVector, out raycast, Color.yellow);

        //自分の真下に向いたベクトル
        Vector3 downVector = transform.position + new Vector3(0,-2,0);
        CreateAndShowOfRay(transform.position, downVector,out raycast, Color.blue);

        //raycastで衝突したメッシュのむき
        Vector3 nomalVector = transform.position + raycast.normal;
        CreateAndShowOfRay(raycast.point, nomalVector,out raycast, Color.green);
    }


    //ベクトルを可視化させて光線をとばす
    public void CreateAndShowOfRay(Vector3 start, Vector3 target,out RaycastHit hit, Color color)
    {
        //Vector3.Cross();　内積　結果Vector3
        //Vector3.dot();    外積 結果float

        //ベクトルの差分
        //差分　＝　ベクトル2　-　ベクトル1
        Vector3 difference = target - start;

        //ベクトルの距離
        float distance = Vector3.Distance(start, target);

        //ベクトルの可視化
        Debug.DrawRay(start,difference,color);

        //光線飛ばす
        //Raycast（開始点、向き、out 、衝突情報、距離、ますく）
        Physics.Raycast(start, difference,out hit,distance,LayerMask.GetMask("Terrain"));
    }

}
