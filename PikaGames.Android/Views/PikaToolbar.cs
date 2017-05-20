using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MediaRouteButton = Android.Support.V7.App.MediaRouteButton;

namespace PikaGames.Android.Views
{
    [Register("PikaGames.Android.Views.PikaToolbar")]
    public class PikaToolbar : LinearLayout
    {
        internal MediaRouteButton MediaRouteButton;

        public PikaToolbar(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            LayoutInflater inflater = (LayoutInflater) context.GetSystemService(Context.LayoutInflaterService);
            inflater.Inflate(Resource.Layout.PikaToolbar, this, true);

            MediaRouteButton = (MediaRouteButton) GetChildAt(0);
        }

        public PikaToolbar(Context context) : this(context, null)
        {
        }
    }
}