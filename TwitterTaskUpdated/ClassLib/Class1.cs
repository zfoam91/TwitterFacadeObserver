namespace Twitter;

public abstract class AbstractSubject{
    protected string status;
    protected  string Name;
    protected List<Observer> subscribers;
    public void NotifyAll(string status){
        foreach(var subscriber in subscribers){
            subscriber.UpdateAll(this, status);
        }
    }
    public void Notify(Observer subscriber, string action){
        subscriber.Update(this, action);
    }
    public AbstractSubject(string name){
        this.status = "";
        subscribers = new List<Observer>();
        this.Name = name;
    }
    public string GetName(){
        return this.Name;
    }
    public void SetStatus(string status){
            this.status = status;
            this.NotifyAll(status);
    }
    public void AttachSubscriber(Observer subscriber){
        this.subscribers.Add(subscriber);
        this.Notify(subscriber, "follow");
    }
    public void RemoveSubscriber(Observer subscriber){
        this.subscribers.Remove(subscriber);
        this.Notify(subscriber, "unfollow");
    }
    public void ShowSubscribers(){
        Console.WriteLine("---------------------");
        Console.WriteLine($"Subscribers of {this.Name}");
        foreach(var subject in subscribers){
            Console.WriteLine(subject.GetName());
        }
        Console.WriteLine("---------------------");
    }
    public abstract void NotifyMe(Observer subscriber, string action);
}

public class Subject: AbstractSubject{
    public Subject(string name):base(name){}
    public override void NotifyMe(Observer subscriber, string action){
        Console.WriteLine("---------------------");
        Console.WriteLine($"{this.Name} Profile");
        if(action == "follow"){
            Console.WriteLine($"{subscriber.GetName()} followed you!");
        }else Console.WriteLine($"{subscriber.GetName()} unfollowed you!");
        Console.WriteLine("---------------------");
        
    }
}

public abstract class Observer{
    public List<AbstractSubject>subscriptions;
    protected string Name;
    public void subscribe(AbstractSubject _subject){
        this.subscriptions.Add(_subject);
        _subject.AttachSubscriber(this);
        _subject.NotifyMe(this, "follow");
    }
    public void unsubscribe(AbstractSubject _subject){
        this.subscriptions.Remove(_subject);
        _subject.RemoveSubscriber(this);
        _subject.NotifyMe(this, "unfollow");
    }
    public void ShowSubscriptions(){
        Console.WriteLine("---------------------");
        Console.WriteLine($"Subscriptions of {this.Name}");
        foreach(var subject in subscriptions){
            Console.WriteLine(subject.GetName());
        }
        Console.WriteLine("---------------------");
    }
    public Observer(string name){
        this.Name = name;
        this.subscriptions = new List<AbstractSubject>();
    }
    public string GetName(){
        return this.Name;
    }
    public abstract void UpdateAll(AbstractSubject subscription, string status);
    public abstract void Update(AbstractSubject subscription, string action); 
}

public class View:Observer{
    public View(string name):base(name){}
    public override void UpdateAll(AbstractSubject subscription, string status)
    {
        Console.WriteLine("---------------------");
        Console.WriteLine($"{this.Name} Profile");
        Console.WriteLine($"{subscription.GetName()} status changed to {status}");
        Console.WriteLine("---------------------");
    }
    public override void Update(AbstractSubject subscription, string action){
        Console.WriteLine("---------------------");
        Console.WriteLine($"{this.Name} Profile");
        if(action == "follow"){
            Console.WriteLine($"You followed {subscription.GetName()}!");
        }else Console.WriteLine($"You unfollowed {subscription.GetName()}!");
        Console.WriteLine("---------------------");
    }
    
}


public class TwitterProfile{
    private string Name;
    public AbstractSubject Me;
    public Observer subscriber;
    public List<TwitterProfile>Following;
    public List<TwitterProfile>Followers;
    public TwitterProfile(string name){
        this.Me = new Subject(name);
        this.Name = name;
        this.subscriber = new View(name);
        this.Followers= new List<TwitterProfile>();
        this.Following=new List<TwitterProfile>(); 
    }
    public void subscribe(TwitterProfile tp){
       this.subscriber.subscribe(tp.Me);
       this.Following.Add(tp);
    }
    public void unsubscribe(TwitterProfile tp){
        this.subscriber.unsubscribe(tp.Me);
        this.Following.Remove(tp);
    }
    public void AddSubscriber(TwitterProfile tp){
        this.Me.AttachSubscriber(tp.subscriber);
        this.Followers.Add(tp);
    }
    public void RemoveSubscriber(TwitterProfile tp){
        this.Me.RemoveSubscriber(tp.subscriber);
        this.Followers.Remove(tp);
    }
    public void SetStatus(string status){
        this.Me.SetStatus(status);
    }
    public void ShowSubscribers(){
        this.Me.ShowSubscribers();
    }
    public void ShowSubscriptions(){
        this.subscriber.ShowSubscriptions();
    }
}
