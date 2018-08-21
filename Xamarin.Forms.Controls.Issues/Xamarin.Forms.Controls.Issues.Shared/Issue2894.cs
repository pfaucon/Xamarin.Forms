using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms.CustomAttributes;
using Xamarin.Forms.Internals;


#if UITEST
using Xamarin.UITest;
using NUnit.Framework;
using Xamarin.Forms.Core.UITests;
#endif

namespace Xamarin.Forms.Controls.Issues
{
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Github, 2894, "[iOS] Gesture Recognizers added to Span after it's been set to FormattedText don't work and can cause an NRE",
		PlatformAffected.iOS)]
#if UITEST
	[NUnit.Framework.Category(UITestCategories.ListView)]
#endif
	public class Issue2894 : TestContentPage
	{
		protected override void Init()
		{
			var label = new Label();

			var s = new FormattedString();
			var span = new Span { Text = "I will fire when clicked", FontAttributes = FontAttributes.Bold };
			var span2 = new Span { Text = "I will not fire when clicked", FontAttributes = FontAttributes.Bold };
			var recognizer = new TapGestureRecognizer()
			{
				Command = new Command(async () =>
				{
					await DisplayAlert("not working", "not working", "not working");
				})
			};

			span.GestureRecognizers.Add(new TapGestureRecognizer()
			{
				Command = new Command(() =>
				{
					//await DisplayAlert("Yay clicked", "yay clicked", "yay clicked");

					if (span2.GestureRecognizers.Contains(recognizer))
						span2.GestureRecognizers.Remove(recognizer);
					else
						span2.GestureRecognizers.Add(recognizer);
				})
			});

			s.Spans.Add(span);
			s.Spans.Add(span2);

			label.FormattedText = s;
			/*span2.GestureRecognizers.Add(new TapGestureRecognizer()
			{
				Command = new Command(async () =>
				{
					await DisplayAlert("not working", "not working", "not working");
				})
			});*/


			Content = new ContentView()
			{
				Content = new StackLayout()
				{
					Children = { label },
					Padding = 40
				}
			};
		}

#if UITEST
		[Test]
		public void GestureWorking()
		{

		}
#endif
	}
}
