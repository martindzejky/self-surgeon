using UnityEngine;

/// <summary>
///   Audio utilities.
///   Copied from an older game at
///   https://github.com/martindzejky/hell-court/blob/master/Assets/Scripts/Tools/AudioUtil.cs
/// </summary>
public class AudioPlayer {

  /// <summary>
  ///   Play a clip.
  /// </summary>
  public static AudioSource Play(AudioClip clip) {
    // find the camera
    var player = FindListener();

    // play
    return PlayAtPosition(player.transform.position, clip);
  }

  /// <summary>
  ///   Play a clip.
  /// </summary>
  public static AudioSource PlayAtPosition(Vector2 position, AudioClip clip) {
    // play
    return CreateAudioObject(position, clip);
  }

  /// <summary>
  ///   Play a clip.
  /// </summary>
  public static AudioSource PlayAtPositionWithPitch(Vector2 position,
                                                    AudioClip clip,
                                                    float pitch = 1f,
                                                    float randomOffset = 0.2f) {
    // create audio
    var audio = CreateAudioObject(position, clip);

    // modify pitch
    audio.pitch = Random.Range(pitch - randomOffset, pitch + randomOffset);

    return audio;
  }

  /// <summary>
  ///   Creates a temporary audio object.
  /// </summary>
  private static AudioSource CreateAudioObject(Vector2 position, AudioClip clip, bool destroy = true, bool play = true) {
    // find player
    var player = FindListener();

    // get distance
    var distance = position - (Vector2) player.transform.position;

    // create game object
    var go = new GameObject("AudioObject");

    // set position
    go.transform.position = position;

    // add audio source
    var audio = go.AddComponent<AudioSource>();

    // set it up
    audio.clip = clip;
    var magnitude = distance.magnitude;
    if (magnitude > 0.01f) {
      audio.volume = Mathf.Lerp(1, 0, magnitude / MaxHearingDistance);
      audio.volume = audio.volume * audio.volume;
      audio.panStereo = Mathf.Lerp(-1, 1, (distance.x / MaxPanDistance + 1) / 2);
    }

    // play
    if (play)
      audio.Play();

    // destroy
    if (destroy)
      Object.Destroy(go, clip.length);

    return audio;
  }

  private static GameObject FindListener() {
    var player = GameObject.FindWithTag("Player");

    if (player) {
      return player;
    } else {
      return GameObject.FindWithTag("MainCamera");
    }
  }

  /// <summary>
  ///   Maximum hearing range.
  /// </summary>
  public static float MaxHearingDistance = 200f;

  /// <summary>
  ///   Where to pan fully.
  /// </summary>
  public static float MaxPanDistance = 64f;

}