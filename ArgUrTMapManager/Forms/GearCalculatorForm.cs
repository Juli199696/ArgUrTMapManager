using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArgUrTMapManager.Forms
{
	public partial class GearCalculatorForm : ToolForm
	{
		#region Enums
		enum GearFlags
		{
			Grenades = 1,
			Snipers = 2,
			Spas = 4,
			Pistols = 8,
			AutomaticGuns = 16,
			Negev = 32
		}

		enum AllowvoteFlags
		{
			Reload						= 1,
			Restart						= 2,
			Map							= 4,
			Nextmap						= 8,
			Kick						= 16,
			SwapTeams					= 32,
			ShuffleTeams				= 64,
			FriendlyFire				= 128,
			FollowStrict				= 256,
			GameType					= 512,
			WaveRespawns				= 1024,
			Timelimit					= 2 << 10,
			FragLimit					= 2 << 11,
			CaptureLimit				= 2 << 12,
			RespawnDelay				= 2 << 13,
			RedWaveRespawnDelay			= 2 << 14,
			BlueWaveRespawnDelay		= 2 << 15,
			BombExplodeTime				= 2 << 16,
			BombDefuseTime				= 2 << 17,
			SurvivorRoundTime			= 2 << 18,
			CaputureScoreTime			= 2 << 19,
			Warmup						= 2 << 20,
			MatchMode					= 2 << 21,
			Timeouts					= 2 << 22,
			TimeoutLength				= 2 << 23,
			Exec						= 2 << 24,
			SwapRoles					= 2 << 25,
			MaxRounds					= 2 << 26,
			Gear						= 2 << 27,
			Cyclemap					= 2 << 28
		}
		#endregion

		public GearCalculatorForm()
			:base()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			this.fcDisabledGuns.SetFlagType(typeof(GearFlags));
			this.fcAllowvote.SetFlagType(typeof(AllowvoteFlags));
		}

		protected override void OnClientSizeChanged(EventArgs e)
		{
			base.OnClientSizeChanged(e);

			int width = (this.ClientSize.Width - 27) / 2;
			this.pDisabledGuns.Width = width;
			this.pAllowvote.Location = new Point(15 + width, 12);
			this.pAllowvote.Width = width;
		}

		private void fcDisabledGuns_ValueChanged(object sender, EventArgs e)
		{
			string text = Constants.CVarSet + " " + Constants.CVarGear + " \"" + this.fcDisabledGuns.Value.ToString() + "\"";
			this.tbDisabledGunsValue.Text = text;
		}

		private void fcAllowvote_ValueChanged(object sender, EventArgs e)
		{
			string text = Constants.CVarSet + " " + Constants.CVarAllowvote + " \"" + this.fcAllowvote.Value.ToString() + "\"";
			this.tbAllowvoteValue.Text = text;
		}
	}
}
