// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("uRfleIig7fBCJxsImGNbHBB1+wAdk0uYvbmnAsvv54b+2K4zrnM+NqRWh0axUf/qTgwUMlrudDGRd+33NlVImVrJOecoWAe8A+x+vz4eROcW+8vEP+wNMk3ErBHeA1QJ9ZdJdSpB2+WBcbGEjzcp/T7xKfmcJPCq7NW8ZiJ1EfAkp4nTkIQnZ/zBMM/4e3V6Svh7cHj4e3t60z6X/qqiDxWu4wmmDStm1uPChf7z/+l+hE8Hs43zASqWMcme6M6iuXsGYGbZU4mTwqc3MRMDvflGfU52lMB0/oZQi0r4e1hKd3xzUPwy/I13e3t7f3p5mF6uCoGLNO58xS51aTM/QWNUelj4RqnKYIJlUlZ3iIFEgz+zIH2zVZAOYUynsshWM3h5e3p7");
        private static int[] order = new int[] { 5,2,8,6,13,9,12,8,12,13,12,13,13,13,14 };
        private static int key = 122;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
