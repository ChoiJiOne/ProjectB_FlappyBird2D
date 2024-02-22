#pragma once

#include "Vec2.h"
#include "Vec3.h"
#include "Vec4.h"


/**
 * @brief 2D 위치와 색상 정보를 가진 정점입니다.
 */
struct VertexPositionColor2D
{
	/**
	 * @brief 2D 위치와 색상 정보를 가진 정점의 기본 생성자입니다.
	 */
	VertexPositionColor2D() noexcept 
		: position(0.0f, 0.0f)
		, color(0.0f, 0.0f, 0.0f, 0.0f) {}


	/**
	 * @brief 2D 위치와 색상 정보를 가진 정점의 생성자입니다.
	 *
	 * @param p 정점의 위치입니다.
	 * @param c 정점의 색상입니다.
	 */
	VertexPositionColor2D(Vec2f&& p, Vec4f&& c) noexcept
		: position(p)
		, color(c) {}


	/**
	 * @brief 2D 위치와 색상 정보를 가진 정점의 생성자입니다.
	 *
	 * @param p 정점의 위치입니다.
	 * @param c 정점의 색상입니다.
	 */
	VertexPositionColor2D(const Vec2f& p, const Vec4f& c) noexcept
		: position(p)
		, color(c) {}


	/**
	 * @brief 2D 위치와 색상 정보를 가진 정점의 생성자입니다.
	 *
	 * @param x 위치의 X좌표입니다.
	 * @param y 위치의 Y좌표입니다.
	 * @param r 정점 색상의 R입니다.
	 * @param g 정점 색상의 G입니다.
	 * @param b 정점 색상의 B입니다.
	 * @param a 정점 색상의 A입니다.
	 */
	VertexPositionColor2D(
		float x, float y, float z,
		float r, float g, float b, float a
	) noexcept
		: position(x, y)
		, color(r, g, b, a) {}


	/**
	 * @brief 2D 위치와 색상 정보를 가진 정점의 생성자입니다.
	 *
	 * @param instance 복사할 정점 인스턴스입니다.
	 */
	VertexPositionColor2D(VertexPositionColor2D&& instance) noexcept
		: position(instance.position)
		, color(instance.color) {}


	/**
	 * @brief 2D 위치와 색상 정보를 가진 정점의 생성자입니다.
	 *
	 * @param instance 복사할 정점 인스턴스입니다.
	 */
	VertexPositionColor2D(const VertexPositionColor2D& instance) noexcept
		: position(instance.position)
		, color(instance.color) {}


	/**
	 * @brief 2D 위치와 색상 정보를 가진 정점의 대입 연산자입니다.
	 *
	 * @param instance 대입할 정점 인스턴스입니다.
	 *
	 * @return 대입한 객체의 참조자를 반환합니다.
	 */
	VertexPositionColor2D& operator=(VertexPositionColor2D&& instance) noexcept
	{
		if (this == &instance) return *this;

		position = instance.position;
		color = instance.color;

		return *this;
	}


	/**
	 * @brief 2D 위치와 색상 정보를 가진 정점의 대입 연산자입니다.
	 *
	 * @param instance 대입할 정점 인스턴스입니다.
	 *
	 * @return 대입한 객체의 참조자를 반환합니다.
	 */
	VertexPositionColor2D& operator=(const VertexPositionColor2D& instance) noexcept
	{
		if (this == &instance) return *this;

		position = instance.position;
		color = instance.color;

		return *this;
	}


	/**
	 * @brief 정점의 바이트 보폭 값을 얻습니다.
	 *
	 * @return 정점의 파이트 보폭(Stride) 값을 반환합니다.
	 */
	static uint32_t GetStride()
	{
		return sizeof(VertexPositionColor2D);
	}


	/**
	 * @brief 정점의 위치입니다.
	 */
	Vec2f position;


	/**
	 * @brief 정점의 색상입니다.
	 */
	Vec4f color;
};