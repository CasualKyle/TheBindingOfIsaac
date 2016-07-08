#pragma once

#include "stdafx.h"
#include "Component.hpp"

class Entity
{
public:
	Entity(sf::Sprite sprite);
	Entity(sf::Sprite sprite, sf::IntRect bounds);

	sf::FloatRect getBounds() const;
	void addComponent(std::unique_ptr<Component> component);
	void removeComponent(Component& component);
	bool hasComponent(Component::ComponentType type) const;
	std::shared_ptr<Component> getComponent(Component::ComponentType type);

	sf::Sprite sprite;
	bool shouldDelete;

private:
	sf::IntRect bounds;
	std::vector<std::shared_ptr<Component>> components;
};

//A possible preformance increase would be to have hasComponent return an index and have getComponent take the index for one less interation cycle