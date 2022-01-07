using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class MetaBalls : MonoBehaviour {



	private Renderer rend;
	private MaterialPropertyBlock materialPropertyBlock;
	private ParticleSystem pSystem;
	private ParticleSystem.Particle[] particles;
	[SerializeField]
	private List<Vector4> particlesPos = new List<Vector4>(100);
	public AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	[SerializeField]
	private Texture2D dataTexture;
	private float speed = 0.0f;
	
	// Use this for initialization
	void Awake () {
		
		pSystem = GetComponent<ParticleSystem>();
		particles = new ParticleSystem.Particle[pSystem.main.maxParticles];
		particlesPos = new List<Vector4>(100);
		rend = pSystem.GetComponent<Renderer>();
		materialPropertyBlock = new MaterialPropertyBlock();
		speed = pSystem.main.startSpeedMultiplier;
	}
	
	// Update is called once per frame
	void Update() {
		// Clear the position of particles.
		// particlesPos.Clear();


		// Get a list of the current alive particles in this frame.
		int aliveParticles = pSystem.GetParticles(particles);		
		dataTexture = new Texture2D(
			128, 
			1, 
			TextureFormat.RGBAFloat, 
			false);
		


		// Get the position of all alive particles in this frame.
		for(int i = 0; i < aliveParticles; i++){

			// Vector4 particleData = i < particles.Length ? particles[i].position : Vector3.zero;
			Vector4 particleData = particles[i].position;
			float lifetime = (particles[i].startLifetime - particles[i].remainingLifetime) / particles[i].startLifetime;
			lifetime = curve.Evaluate(lifetime) * particles[i].GetCurrentSize(pSystem);

			particleData.w		 = lifetime;

			Color dataColor = new Color(
				particleData.x,
				particleData.y,
				particleData.z,
				particleData.w
			);
			
			
			dataTexture.SetPixel(i, 1, dataColor);
			// particlesPos.Add(particleData);
		}

		dataTexture.Apply();

		// Update the position array in the shader.

		// Shader.SetGlobalVectorArray("_ParticlePos", particlesPos);
		materialPropertyBlock.SetFloat("_DataTexelSize", dataTexture.texelSize.x);
		materialPropertyBlock.SetTexture("_DataTexture", dataTexture);
		rend.SetPropertyBlock(materialPropertyBlock);

	}

}
