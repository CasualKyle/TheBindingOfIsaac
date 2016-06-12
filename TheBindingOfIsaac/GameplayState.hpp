#pragma once

#include "stdafx.h"
#include "State.hpp"
#include "StatsState.hpp"
#include "Map.hpp"
#include "HUD.hpp"

#include "Assemblages.hpp"
#include "RenderSystem.hpp"
#include "InputSystem.hpp"
#include "PhysicsSystem.hpp"
#include "ProjectileSystem.hpp"

class GameplayState : public State
{
public:
	GameplayState(StatsState& statsState, sf::RenderWindow& window, const sf::Time& deltaTime);

	void initialize();

	void update(sf::Time deltaTime);
	void draw(sf::RenderWindow& window);

private:
	StatsState& statsState;
	HUD hud;
	Map map;

	RenderSystem renderSystem;
	InputSystem inputSystem;
	PhysicsSystem physicsSystem;
	ProjectileSystem projectileSystem;
};