#pragma once

#include "stdafx.h"
#include "State.hpp"

class GameManager
{
public:
	static GameManager& getInstance();

	void initialize();
	void gameLoop();
	void addState(std::shared_ptr<State> state);
	void resetGameplay();

	sf::RenderWindow& getWindow();
	bool getWindowHasFocus() const;
	const sf::Time& getTimeStep() const;

private:
	GameManager();
	GameManager(const GameManager&);
	void operator=(const GameManager&);

	sf::RenderWindow window;
	bool windowHasFocus;
	std::vector<std::shared_ptr<State>> states;
	std::vector<std::shared_ptr<State>> statesToAdd;
	sf::Time elapsedTime;
	const sf::Time timeStep;
	sf::Clock clock;
};