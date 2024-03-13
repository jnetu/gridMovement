using UnityEngine;
using UnityEngine.UI;

public class MenuControll : MonoBehaviour
{
    public int local = 0;
    int maxlocal;
    public RectTransform rectTransform;
    public RectTransform[] AllNavigationsButtons; // precisa adicionar os itens navegados

[SerializeField] private float DISTANCE = 0.38f; // distancia do seletor para os botoes 

    public MenuLoadScene menuSelect;

    public Sprite disselected;
    public Sprite selected;

 public float DISTANCE1 { get => DISTANCE; set => DISTANCE = value; }

    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        maxlocal = AllNavigationsButtons.Length - 1;

        foreach (var item in AllNavigationsButtons)
        {
            item.gameObject.GetComponent<Button>().onClick.AddListener(delegate { ExampleFunction(item.name); });
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            local--;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {

            local++;

        }
        if (Input.GetKeyDown(KeyCode.Return))
        {

            AllNavigationsButtons[local].gameObject.GetComponent<Button>().onClick.Invoke();
            
        }

        if (local > maxlocal)
        {
            local = 0;
        }
        if (local < 0)
        {
            local = maxlocal;
        }

        updateSprite();

        



        VerifyLocal();

    }
    void updateSprite()
    {
        
        int i = 0;
        foreach (var item in AllNavigationsButtons)
        {
            
            if (i !=local)
            {
                AllNavigationsButtons[i].gameObject.GetComponent<Image>().sprite = disselected;
            }
            else
            {
                AllNavigationsButtons[i].gameObject.GetComponent<Image>().sprite = selected;
            }
            i++;

        }
    }

    void VerifyLocal()
    {
        try
        {
            Vector3 buttonPosition = AllNavigationsButtons[local].position;
            //update selector position
            rectTransform.position = buttonPosition + (buttonPosition.x * Vector3.left * DISTANCE);
        }
        catch (System.IndexOutOfRangeException)
        {
            throw;
        }
    }

    public void ExampleFunction(string button)
    {
        Debug.Log("click event on: " + button);
        if (button == "Settings")
        {
            //load menu configuration
        }
        else if (button == "Exit")
        {
            Application.Quit();
        }
        else if(button == "Start")
        {
            menuSelect = new MenuLoadScene(button);
        }
        else
        {
            //erro se aparecer esse
            return;
        }

    }
}
