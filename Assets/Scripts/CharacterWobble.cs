using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

    public class CharacterWobble : MonoBehaviour
    {
        TMP_Text textMesh;

        Mesh mesh;

        Vector3[] vertices;

        List<WiggleStartAndEnd> wiggleStartAndEnds = new List<WiggleStartAndEnd>();
        int count = 0;
        float refreshSpeed = 0.5f;

        string text;
        int indexInText = 0;
        float timer = 0;

        [SerializeField] float typeWriterEffectSpeed;
        [SerializeField] bool autoStart;

        void Start()
        {
            textMesh = GetComponent<TMP_Text>();
            text = "";

            if (autoStart)
                SetText(textMesh.text);
        }

        public void SetText(string newText)
        {
            wiggleStartAndEnds.Clear();

            text = newText;

            indexInText = 0;
            int startIndex = -1;
            int endIndex = -1;
            int totalRemoval = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '@' && startIndex == -1)
                {
                    startIndex = i;
                }
                else if (text[i] == '@' && endIndex == -1)
                {
                    endIndex = i;
                    totalRemoval++;
                    wiggleStartAndEnds.Add(new WiggleStartAndEnd(startIndex - totalRemoval, endIndex - totalRemoval));
                    totalRemoval++;
                    startIndex = -1;
                    endIndex = -1;
                }
            }

            for (int i = text.Length - 1; i > 0; i--)
            {
                if (text[i] == '@')
                {
                    text = text.Remove(i, 1);
                }
            }

            textMesh.text = "";
            count = 0;
        }

        void Update()
        {
            timer += Time.deltaTime;

            if (timer > typeWriterEffectSpeed)
            {
                if (indexInText < text.Length)
                {
                    textMesh.text += text[indexInText];
                    indexInText++;
                    timer = 0;
                }
            }

            textMesh.ForceMeshUpdate();
            mesh = textMesh.mesh;
            vertices = mesh.vertices;

            for (int i = 0; i < wiggleStartAndEnds.Count; i++)
            {
                Wiggle(wiggleStartAndEnds[i].startIndex, wiggleStartAndEnds[i].endIndex);
            }

            textMesh.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
            count++;
            mesh.vertices = vertices;
            textMesh.canvasRenderer.SetMesh(mesh);
        }

        Vector2 Wobble(float time)
        {
            return new Vector2(0, Mathf.Sin(time * 4.3f) * 4.3f);
        }

        void Wiggle(int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                if (textMesh.text.Length - 1 < i)
                    break;

                TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];

                if (c.character == ' ')
                    continue;

                int index = c.vertexIndex;

                Vector3 offset = Wobble(Time.time + i);
                vertices[index + 0] += offset;
                vertices[index + 1] += offset;
                vertices[index + 2] += offset;
                vertices[index + 3] += offset;

                //string hexcolor = Rainbow(textMesh.textInfo.characterCount * 5, i + count + (int)Time.deltaTime);
                // Color32 myColor32 = hexToColor(hexcolor);
                int meshIndex = textMesh.textInfo.characterInfo[i].materialReferenceIndex;
                int vertexIndex = textMesh.textInfo.characterInfo[i].vertexIndex;
                // Color32[] vertexColors = textMesh.textInfo.meshInfo[meshIndex].colors32;
                // vertexColors[vertexIndex + 0] = myColor32;
                // vertexColors[vertexIndex + 1] = myColor32;
                // vertexColors[vertexIndex + 2] = myColor32;
                // vertexColors[vertexIndex + 3] = myColor32;
            }
        }

        public static Color32 hexToColor(string hex)
        {
            hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
            hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
            byte a = 255;//assume fully visible unless specified in hex
            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            //Only use alpha if the string has enough characters
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            }
            return new Color32(r, g, b, a);
        }

        /* public static string Rainbow(int numOfSteps, int step)
         {
             var r = 0.0;
             var g = 0.0;
             var b = 0.0;
             var h = (double)step / numOfSteps;
             var i = (int)(h * 6);
             var f = h * 6.0 - i;
             var q = 1 - f;
             switch (i % 6)
             {
                 case 0:
                     r = 1;
                     g = f;
                     b = 0;
                     break;
                 case 1:
                     r = q;
                     g = 1;
                     b = 0;
                     break;
                 case 2:
                     r = 0;
                     g = 1;
                     b = f;
                     break;
                 case 3:
                     r = 0;
                     g = q;
                     b = 1;
                     break;
                 case 4:
                     r = f;
                     g = 0;
                     b = 1;
                     break;
                 case 5:
                     r = 1;
                     g = 0;
                     b = q;
                     break;
             }
             return "#" + ((int)(r * 255)).ToString("X2") + ((int)(g * 255)).ToString("X2") + ((int)(b * 255)).ToString("X2");
         }*/
    }

class WiggleStartAndEnd
{
    public int startIndex;
    public int endIndex;

    public WiggleStartAndEnd(int startIndex, int endIndex)
    {
        this.startIndex = startIndex;
        this.endIndex = endIndex;
    }
};
