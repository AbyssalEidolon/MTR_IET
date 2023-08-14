This should belong on a GameObject/Prefab along with the following:
	Box Collider (Set to trigger)

On start this script should:
	Spawn all [[JointTargetCheckers]]
	::Spawn all [[JointTargets]]

On anything entering it's trigger it should:
	Check if object is valid target (For example, a tool),
	Load positions and toggles from [[Tool Grip Config]],
	Set activity and positions of all Joint-Targets.

When all checks are met it should:
	snap tool to hand 
	visualise hand.
	counter-clamp all target checkers to targets to create pose ranges.