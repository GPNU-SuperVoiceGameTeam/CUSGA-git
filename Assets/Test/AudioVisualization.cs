using UnityEngine;

public class AudioVisualization : MonoBehaviour
{
    // Public variable to assign an audio clip in the Unity Inspector
    public AudioClip audioClip;
    // Private variable to hold the AudioSource component
    private AudioSource audioSource;
    // Array to store the audio samples
    private float[] samples = new float[128]; // Reduced to 128 for better visualization

    // Start is called before the first frame update
    void Start()
    {
        // Create an empty game object to hold the audio source
        GameObject audioObject = new GameObject("Audio Source");
        // Add an AudioSource component to the newly created game object
        audioSource = audioObject.AddComponent<AudioSource>();
        // Assign the provided audio clip to the AudioSource
        audioSource.clip = audioClip;
        // Set the audio to loop continuously
        audioSource.loop = true;
        // Play the audio clip
        audioSource.Play();

        // Generate 128 rectangle objects as children to visualize the audio spectrum in a circle
        for (int i = 0; i < samples.Length; i++)
        {
            // Create a new rectangle and set its parent to the current game object
            GameObject rect = new GameObject("Rectangle " + i);
            SpriteRenderer spriteRenderer = rect.AddComponent<SpriteRenderer>();
            spriteRenderer.color = Color.white; // Set color of the rectangle
            rect.transform.parent = transform;

            // Calculate the angle for positioning the rectangle in a circle
            float angle = i * (360f / samples.Length);
            // Convert the angle to radians
            float radian = angle * Mathf.Deg2Rad;
            // Position each rectangle around the circle with a radius of 5 units
            rect.transform.localPosition = new Vector3(Mathf.Cos(radian) * 5f, Mathf.Sin(radian) * 5f, 0); // Position rectangles in a circle
            // Rotate the rectangle to face upwards
            rect.transform.rotation = Quaternion.Euler(90, 0, angle); // Rotate the rectangle to align with the y-axis
            // Scale down the rectangle initially
            rect.transform.localScale = new Vector3(0.1f, 0.1f, 1f); // Initial scale of the rectangle
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get spectrum data from the audio source using BlackmanHarris window function
        audioSource.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);

        // Loop through each sample in the array
        for (int i = 0; i < samples.Length; i++)
        {
            // Calculate the y scale based on the absolute value of the sample multiplied by 10
            float yScale = Mathf.Abs(samples[i]) * 10f; // Calculate the y scale for visualization
            // Get the child transform at index i
            Transform child = transform.GetChild(i); // Retrieve the child transform at index i
            // Check if the child exists (though it should always exist due to initialization)
            if (child != null) // Ensure the child exists
            {
                // Set the local scale of the child, adjusting its y size based on the calculated y scale
                child.localScale = new Vector3(0.1f, yScale, 1f); // Adjust the scale of the child
            }
        }
    }
}