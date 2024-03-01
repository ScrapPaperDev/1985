namespace Disparity
{
	public class AudioPlayer
	{
		private ISoundPlayer audioPlayer;

		public static AudioPlayer instance;

		public AudioPlayer(ISoundPlayer player)
		{
			if (instance != null)
				return;

			instance = this;
			audioPlayer = player;
		}

		public void PlaySound(ISoundProvider s)
		{
			audioPlayer.PlaySound(s);
		}
	}

	public interface ISoundPlayer
	{
		void PlaySound(ISoundProvider clip);
	}

	public interface ISoundProvider
	{
		T GetSound<T>() where T : class;
	}

}