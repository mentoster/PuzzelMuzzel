
using UnityEngine;
using UnityEngine.Serialization;

public class ColorManager : MonoBehaviour
{
  
  public Color[] MaterialPalitre;
  public Color[] FullMatPallette;
  public Color[] Pallete;
  private void Awake()
  {
    staticsColor.MatPalette = MaterialPalitre;
    staticsColor.Pallete = Pallete;
    staticsColor.FullMatPallette = FullMatPallette;
    staticsColor.RndPalleteColor=3;
  }
}
