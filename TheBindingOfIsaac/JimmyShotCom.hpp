#pragma once

#include "stdafx.h"
#include "Component.hpp"

class JimmyShotCom : public Component
{
public:
	enum Variation {
		Direct,
		Round,
		DirectRocket
	};

	JimmyShotCom(Variation variation);

	Component::ComponentType getType() const;

	const Variation variation;
	float currentShotTime;
	const float randShotTimeAmount;
	float maxShotTime;
	const float projectileSpeed;
};