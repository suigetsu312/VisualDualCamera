using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class DataSender : MonoBehaviour
{
    [Inject]
    private DualCamera DualCamera { get; set; }
    private bool _isSending;
    private UdpClient UdpClient { get; set; } = new UdpClient();
    private IPEndPoint _udpEndPoint;

    // Start is called before the first frame update
    void Start()
    {
        _isSending = true;
        _udpEndPoint = new IPEndPoint(IPAddress.Loopback, 55555);
        UdpClient.Connect(_udpEndPoint);
        StartSendingData();
    }

    async void StartSendingData()
    {
        while (_isSending)
        {
            SendData();
            await Task.Delay(1000 / 30); // 30Hz
        }
    }

    private async void SendData()
    {
        var payload = DualCamera.GetDualCameraParameter();
        Debug.Log(DualCamera);

        if (payload == null) 
        {
            Debug.Log("拿到空的");
            return; 
        }
        var resultJson = JsonUtility.ToJson(payload);

        await UdpClient.SendAsync(Encoding.UTF8.GetBytes(resultJson), resultJson.Length);
    }

    private void OnDestroy()
    {
        _isSending = false;
    }
}
