using UnityEngine.SocialPlatforms;
using Zenject;

namespace VisualDualCamera
{
    public class IntrinsicParameter
    {
        public double FocalX { get; set; }
        public double FocalY { get; set; }
        public double CenterX { get; set; }
        public double CenterY { get; set; }

        public override string ToString()
        {
            return $"Fx : {FocalX}; Fy : {FocalX}; Cx : {CenterX}; Cy : {CenterY};\n";
        }


    }
    public class CameraPayload
    {
        public IntrinsicParameter IntrinsicParameter { get; set; }
        public string ImgRaw { get; set; }

    }

}

