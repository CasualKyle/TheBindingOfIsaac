#include "stdafx.h"
#include "Utilities.hpp"
#include "Entity.hpp"
#include "PlayerControlledCom.hpp"
#include "VelocityCom.hpp"
#include "PitCollisionCom.hpp"
#include "WallCollisionCom.hpp"

class Assemblages
{
public:
	static Assemblages& getInstance();

	void initialize();

	Entity createPlayer();
	Entity createProjectile(sf::Vector2f position, sf::Vector2f velocity);

private:
	Assemblages();
	Assemblages(const Assemblages&);
	void operator=(const Assemblages&);

	sf::Texture playerSpriteSheet;
	sf::Texture projectilesSpriteSheet;
};