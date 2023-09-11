using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Nobi.UiRoundedCorners;

public abstract class Object {
    public ObjectProperty obj;
    public float eixoX;
    public float eixoY;
    public float eixoZ = 0;
    public int escala;
    public float width;
    public float height;
    public GameObject gameObject;

    public Object(ObjectProperty obj, int escala) {
        this.obj = obj;
        this.escala = escala;
    }

    public void setSize(RectTransform rectTransform) {
        width = obj.absoluteBoundingBox.width/escala;
        height = obj.absoluteBoundingBox.height/escala;
        if(obj.strokeWeight != 0 && obj.strokes.Length == 1 && obj.strokes[0].visible == true) {
            width -= 2*obj.strokeWeight/(float)escala;
            height -= 2*obj.strokeWeight/(float)escala;
        }
        rectTransform.sizeDelta = new Vector2(width, height);
    }

    public void setPosition(RectTransform rectTransform) {
        float diferencaX = (Global.objAbsoluteX * -1) - ((obj.absoluteBoundingBox.x * -1) / escala);
        float diferencaY = (Global.objAbsoluteY * -1) - ((obj.absoluteBoundingBox.y * -1) / escala);
        eixoX = ((obj.absoluteBoundingBox.width / escala) / 2) + diferencaX + (Global.objPositionX - (Global.objWidth/2));
        eixoY = ((obj.absoluteBoundingBox.height / escala) / 2) + diferencaY + (Global.objPositionY - (Global.objHeigth/2));
        eixoZ = Global.objPositionZ;
        rectTransform.localPosition = new Vector3(eixoX, eixoY, eixoZ);
    }

    public void setRotation(RectTransform rectTransform) {
        rectTransform.localRotation = Quaternion.Euler(Global.objRotationX, Global.objRotationY, 0);
    }

    public void setColor(Image painel) {
        if(obj.type == "VECTOR") {
            setVector(painel);
        }
        
        else if(obj.fills.Length == 0 || obj.fills[0].visible == false) {
            painel.color = new Color32(0, 0, 0, 0);
        }
        
        else if(obj.fills.Length == 1){
            if(obj.fills[0].type == "IMAGE") {
                setImage(painel);
            }

            else if(obj.fills[0].type == "SOLID") {
                float r = obj.fills[0].color.r;
                float g = obj.fills[0].color.g;
                float b = obj.fills[0].color.b;
                float a = obj.fills[0].color.a;
                Color32 color = new Color32((byte)(r * 255), (byte)(g * 255), (byte)(b * 255), (byte)(a * 255));
                painel.color = color;
            }
        }
    }

    public void setImage(Image painel) {
        string imageRef = obj.fills[0].imageRef;
        string imageUrl = Global.apiImage;
        imageUrl = imageUrl.Remove(0, Global.apiImage.IndexOf(imageRef));
        imageUrl = imageUrl.Remove(0, imageUrl.IndexOf("https://"));
        imageUrl = imageUrl.Remove(imageUrl.IndexOf('\"'));
        string contentType = APIService.ContentType(imageUrl);
        string path = $"Assets/Figma Convert/Images/{imageRef}.{contentType}";
        if(!APIService.DownloadImage(imageUrl, path)) {
            Debug.Log("Erro no Download da Imagem");
            return;
        }
        if(!System.IO.File.Exists(path)) {
            Debug.Log("Erro no Arquivo de Material");
            return;
        }
        byte[] readImg = System.IO.File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(readImg);
        Rect rec = new Rect(0, 0, texture.width, texture.height);
        Sprite sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
        painel.sprite = sprite;
        gameObject.transform.Rotate(180.0f, 0f, 0f, Space.World);
    }

    public void setVector(Image painel) {
        string id = obj.id;
        string imageUrl = APIService.GetImageID(id);

        if(imageUrl.IndexOf(id + "\":null") > 0) {
            if(id.IndexOf(';') < 0) return;
            id = id.Remove(id.IndexOf(';'));
            imageUrl = APIService.GetImageID(id);
        }

        imageUrl = imageUrl.Remove(0, imageUrl.IndexOf("http"));
        imageUrl = imageUrl.Remove(imageUrl.IndexOf("\"}}"));
        if(id.IndexOf(';') > 0) {
            id = id.Remove(id.IndexOf(';'));
        }
        id = id.Remove(id.IndexOf(":"), 1);
        string path = $"Assets/Figma Convert/Images/{id}.png";
            
        if(!APIService.DownloadImage(imageUrl, path)) {
            Debug.Log("Erro no Download da Imagem");
            return;
        }
        if(!System.IO.File.Exists(path)) {
            Debug.Log("Erro no Arquivo de Material");
            return;
        }

        byte[] readImg = System.IO.File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(readImg);
        Rect rec = new Rect(0, 0, texture.width, texture.height);
        Sprite sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
        painel.sprite = sprite;
        gameObject.transform.Rotate(180.0f, 0f, 0f, Space.World);
    }

    public void setBorder() {
        if(obj.strokeWeight != 0 && obj.strokes.Length == 1) {
            if(obj.strokes[0].visible == true) {
                float strokeWeight = obj.strokeWeight/(float)escala;
                Outline outline = gameObject.AddComponent<Outline>();
                outline.effectDistance = new Vector2(strokeWeight, strokeWeight);
                float r = obj.strokes[0].color.r;
                float g = obj.strokes[0].color.g;
                float b = obj.strokes[0].color.b;
                float a = obj.strokes[0].color.a;
                Color32 color = new Color32((byte)(r * 255), (byte)(g * 255), (byte)(b * 255), (byte)(a * 255));
                outline.effectColor = color;
            }
        }
    }

    public void setCornerRadius() {
        ImageWithRoundedCorners cornersRounded = gameObject.AddComponent<ImageWithRoundedCorners>();
        cornersRounded.radius = obj.cornerRadius/escala;
    }
}