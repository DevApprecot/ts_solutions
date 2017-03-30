//ViewController Functions
public override void ViewDidLoad()
{
	base.ViewDidLoad();
}

public override void ViewWillAppear(bool animate)
{
	base.ViewWillAppear(animate);
	Reachability.ResetInternetEvents();
	Reachability.ReachabilityChanged += Reachability_ReachabilityChanged;
	CreatePresenter();
	AddHandlers();
}

public override void ViewDidAppear(bool animated)
{
	base.ViewDidAppear(animated);
	ToggleConnectionIndicator(IsOnline());
}

public override void ViewWillLayoutSubviews()
{
	base.ViewWillLayoutSubviews();
}

public override void ViewDidDisappear(bool animated)
{
	base.ViewDidDisappear(animated);
	RemoveHandlers();
}

void AddHandlers()
{
	
}

void RemoveHandlers()
{
	
}

