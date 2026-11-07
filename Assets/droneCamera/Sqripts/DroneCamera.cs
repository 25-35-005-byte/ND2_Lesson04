using UnityEngine;
using UnityEngine.InputSystem;

public class DroneCamera : MonoBehaviour
{
    //フィールド 
    [System.Serializable]
    public class Peremeter
    { 
        //カメラのオプション変数をまとめる内部クラス
        //Peremeterのくらす
        public Vector3 position = new Vector3(0, 0, -10); 　//座標
        public Vector3 Angles = Vector3.zero;               //角度
        public float Distance = 10;         //対象との距離
        public float DistanceMin = 0;       //対象との距離限界 さいしょう
        public float DistanceMax = 10;                       //さいだい
        public float TiltMin = -90;         //チルト制限　さいしょう
        public float TiltMax = 90;          //さいだい
        public Vector3 PositionOffset = Vector3.zero;
        public Vector3 AnglesOffset = Vector3.zero;
    }

    //インスペクター表示用の変数
    public Peremeter parameter;

    //カメラの本体
    [Header("Camera Settings")]
    public Transform Joint;
    public Camera Camera;
    public Transform TrackingTarget;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //追いかける
        parameter.position = TrackingTarget.position;

        //マウス操作
        Vector2 inputAxis = Mouse.current.delta.value;
        parameter.Angles.x += inputAxis.y * -1;
        parameter.Angles.y += inputAxis.x;

        Vector2 wheel = Mouse.current.scroll.value;
        parameter.Distance += wheel.y * -1;
    }

   void LateUpdate()
    {
        //カメラの計算結果
        transform.position = parameter.position;

        parameter.Angles.x = Mathf.Clamp
            (
            parameter.Angles.x,
            parameter.TiltMin,
            parameter.TiltMax
            );
        transform.eulerAngles = parameter.Angles;

        parameter.Distance = Mathf.Clamp(
            parameter.Distance,
            parameter.DistanceMin,
            parameter.DistanceMax
            );
        Joint.localPosition = Vector3.forward * -parameter.Distance;

        Camera.transform.localPosition = parameter.PositionOffset;
        Camera.transform.localEulerAngles = parameter.AnglesOffset;
    }
}
