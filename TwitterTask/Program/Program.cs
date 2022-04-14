using Twitter;
using System;
TwitterProfile Profile1 = new TwitterProfile("User1");
TwitterProfile Profile2 = new TwitterProfile("User2");
TwitterProfile Profile3 = new TwitterProfile("User3");
TwitterProfile Profile4 = new TwitterProfile("User4");
TwitterProfile Profile5 = new TwitterProfile("User5");

Profile1.subscribe(Profile2);
Profile1.subscribe(Profile3);
Profile2.subscribe(Profile1);
Profile4.subscribe(Profile1);
Profile5.subscribe(Profile1);
Profile2.subscribe(Profile2);

Profile1.ShowSubscribers();
Profile1.ShowSubscriptions();

Profile1.SetStatus(":D");
Profile4.unsubscribe(Profile1);
Profile1.SetStatus("D:");

Profile1.ShowSubscribers();


