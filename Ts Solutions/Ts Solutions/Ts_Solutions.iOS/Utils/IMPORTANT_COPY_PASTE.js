//ViewController Functions
public override void ViewDidLoad()
{
	base.ViewDidLoad();
	DismissKeyboardOnBackgroundTap();
}

public override void ViewWillAppear(bool animate)
{
	base.ViewWillAppear(animate);
	CreatePresenter();
	Reachability.ResetInternetEvents();
	Reachability.ReachabilityChanged += Reachability_ReachabilityChanged;
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

public async void Reachability_ReachabilityChanged(object sender, EventArgs e)
{
	await OnConnected();
}

public override async Task OnConnected()
{
 	ToggleConnectionIndicator(IsOnline());
	if (IsOnline())
		await _presenter.
}

void AddHandlers()
{
	
}

void RemoveHandlers()
{
	
}

