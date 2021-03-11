using UnityEngine;

using NativeWebSocket;
using UnityEngine.UI;
using System.Threading.Tasks;

public class Connection : MonoBehaviour
{
  [SerializeField] Button connect;
  [SerializeField] Button close;
  [SerializeField] Button sendbt;
  [SerializeField] Button sendstr;
  [SerializeField] InputField inputField;
  WebSocket websocket;
  bool ispause = false;
  // Start is called before the first frame update
  void Start()
  {
    connect.onClick.AddListener(ConnectServerAsync);

    close.onClick.AddListener(() =>
    {
      _ = websocket.Close();
      websocket = null;
    });
    sendbt.onClick.AddListener(()=>
    {
      SendWebSocketMessagebt();
    });
    sendstr.onClick.AddListener(() =>
    {
      SendWebSocketMessagest();
    });
  }

  void ConnectServerAsync()
  {
    if (null == websocket)
    {
      websocket = new WebSocket(inputField.text);

      websocket.OnOpen += () =>
      {
        Debug.Log("Connection open!");
      };

      websocket.OnError += (e) =>
      {
        Debug.Log("Error! " + e);
      };

      websocket.OnClose += (e) =>
      {
        Debug.Log("Connection closed!");
      };

      websocket.OnMessage += (bytes) =>
      {
        // Reading a plain text message
        var message = System.Text.Encoding.UTF8.GetString(bytes);
        Debug.Log("Received OnMessage! (" + bytes.Length + " bytes) " + message);
      };
    }
    _= websocket.Connect();
  }

  void Update()
  {
#if !UNITY_WEBGL || UNITY_EDITOR
    if (null != websocket)
    {
      websocket.DispatchMessageQueue();
    }
#endif
  }

  async void SendWebSocketMessagebt()
  {
    if (ispause) return;
    if (websocket.State == WebSocketState.Open)
    {
      // Sending bytes
      await websocket.Send(new byte[] { 10, 20, 30 });
      Debug.Log($"{nameof(Connection)}: Send Byte Message Finished");
    }
  }
  async void SendWebSocketMessagest()
  {
    if (websocket.State == WebSocketState.Open)
    {
      // Sending plain text
      await websocket.SendText("plain text message");
      Debug.Log($"{nameof(Connection)}: Send String Message Finished");
    }
  }

  private async void OnApplicationQuit()
  {
    await websocket.Close();
  }
}
