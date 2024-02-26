#pragma once

#include <cstdint>


/**
 * @brief 템플릿 기반의 3x3 행렬입니다.
 */
template <typename T>
struct TMat3x3
{
	/**
	 * @brief 3x3 행렬의 기본 생성자입니다.
	 *
	 * @note 모든 원소의 값을 0으로 초기화합니다.
	 */
	TMat3x3() noexcept
	{
		e00 = static_cast<T>(0); e01 = static_cast<T>(0); e02 = static_cast<T>(0);
		e10 = static_cast<T>(0); e11 = static_cast<T>(0); e12 = static_cast<T>(0);
		e20 = static_cast<T>(0); e21 = static_cast<T>(0); e22 = static_cast<T>(0);
	}


	/**
	 * @brief 3x3 행렬의 생성자입니다.
	 *
	 * @param ee00 행렬의 (0, 0) 성분입니다.
	 * @param ee01 행렬의 (0, 1) 성분입니다.
	 * @param ee02 행렬의 (0, 2) 성분입니다.
	 * @param ee10 행렬의 (1, 0) 성분입니다.
	 * @param ee11 행렬의 (1, 1) 성분입니다.
	 * @param ee12 행렬의 (1, 2) 성분입니다.
	 * @param ee20 행렬의 (2, 0) 성분입니다.
	 * @param ee21 행렬의 (2, 1) 성분입니다.
	 * @param ee22 행렬의 (2, 2) 성분입니다.
	 */
	TMat3x3(
		T&& ee00, T&& ee01, T&& ee02,
		T&& ee10, T&& ee11, T&& ee12,
		T&& ee20, T&& ee21, T&& ee22
	) noexcept
	{
		e00 = ee00; e01 = ee01; e02 = ee02;
		e10 = ee10; e11 = ee11; e12 = ee12;
		e20 = ee20; e21 = ee21; e22 = ee22;
	}


	/**
	 * @brief 3x3 행렬의 생성자입니다.
	 *
	 * @param ee00 행렬의 (0, 0) 성분입니다.
	 * @param ee01 행렬의 (0, 1) 성분입니다.
	 * @param ee02 행렬의 (0, 2) 성분입니다.
	 * @param ee10 행렬의 (1, 0) 성분입니다.
	 * @param ee11 행렬의 (1, 1) 성분입니다.
	 * @param ee12 행렬의 (1, 2) 성분입니다.
	 * @param ee20 행렬의 (2, 0) 성분입니다.
	 * @param ee21 행렬의 (2, 1) 성분입니다.
	 * @param ee22 행렬의 (2, 2) 성분입니다.
	 */
	TMat3x3(
		const T& ee00, const T& ee01, const T& ee02,
		const T& ee10, const T& ee11, const T& ee12,
		const T& ee20, const T& ee21, const T& ee22
	) noexcept
	{
		e00 = ee00; e01 = ee01; e02 = ee02;
		e10 = ee10; e11 = ee11; e12 = ee12;
		e20 = ee20; e21 = ee21; e22 = ee22;
	}


	/**
	 * @brief 3x3 행렬의 원소를 하나의 값으로 초기화합니다.
	 *
	 * @param e 모든 원소를 초기화 할 값입니다.
	 */
	TMat3x3(T&& e) noexcept
	{
		e00 = e; e01 = e; e02 = e;
		e10 = e; e11 = e; e12 = e;
		e20 = e; e21 = e; e22 = e;
	}


	/**
	 * @brief 3x3 행렬의 원소를 하나의 값으로 초기화합니다.
	 *
	 * @param e 모든 원소를 초기화 할 값입니다.
	 */
	TMat3x3(const T& e) noexcept
	{
		e00 = e; e01 = e; e02 = e;
		e10 = e; e11 = e; e12 = e;
		e20 = e; e21 = e; e22 = e;
	}


	/**
	 * @brief 3x3 행렬의 복사 생성자입니다.
	 *
	 * @param m 원소를 복사할 행렬의 인스턴스입니다.
	 */
	TMat3x3(TMat3x3<T>&& m) noexcept
	{
		e00 = m.e00; e01 = m.e01; e02 = m.e02;
		e10 = m.e10; e11 = m.e11; e12 = m.e12;
		e20 = m.e20; e21 = m.e21; e22 = m.e22;
	}


	/**
	 * @brief 3x3 행렬의 복사 생성자입니다.
	 *
	 * @param m 원소를 복사할 행렬의 인스턴스입니다.
	 */
	TMat3x3(const TMat3x3<T>& m) noexcept
	{
		e00 = m.e00; e01 = m.e01; e02 = m.e02;
		e10 = m.e10; e11 = m.e11; e12 = m.e12;
		e20 = m.e20; e21 = m.e21; e22 = m.e22;
	}


	/**
	 * @brief 3x3 행렬의 대입 연산자입니다.
	 *
	 * @param m 원소를 복사할 행렬의 인스턴스입니다.
	 *
	 * @return 대입한 행렬의 참조자를 반환합니다.
	 */
	TMat3x3<T>& operator=(TMat3x3<T>&& m) noexcept
	{
		if (this == &m) return *this;

		e00 = m.e00; e01 = m.e01; e02 = m.e02;
		e10 = m.e10; e11 = m.e11; e12 = m.e12;
		e20 = m.e20; e21 = m.e21; e22 = m.e22;

		return *this;
	}


	/**
	 * @brief 3x3 행렬의 대입 연산자입니다.
	 *
	 * @param m 원소를 복사할 행렬의 인스턴스입니다.
	 *
	 * @return 대입한 행렬의 참조자를 반환합니다.
	 */
	TMat3x3<T>& operator=(const TMat3x3<T>& m) noexcept
	{
		if (this == &m) return *this;

		e00 = m.e00; e01 = m.e01; e02 = m.e02;
		e10 = m.e10; e11 = m.e11; e12 = m.e12;
		e20 = m.e20; e21 = m.e21; e22 = m.e22;

		return *this;
	}


	/**
	 * @brief 3x3 행렬의 모든 원소에 -부호를 취합니다.
	 *
	 * @return 모든 원소에 -부호를 취한 새로운 행렬을 반환합니다.
	 */
	TMat3x3<T> operator-() const
	{
		return TMat3x3<T>(
			-e00, -e01, -e02,
			-e10, -e11, -e12,
			-e20, -e21, -e22
			);
	}


	/**
	 * @brief 두 3x3 행렬의 대응하는 원소를 더합니다.
	 *
	 * @param m 행렬의 덧셈을 수행할 피연산자입니다.
	 *
	 * @return 두 행렬의 대응하는 원소를 더한 결과를 반환합니다.
	 */
	TMat3x3<T> operator+(TMat3x3<T>&& m) const
	{
		return TMat3x3<T>(
			e00 + m.e00, e01 + m.e01, e02 + m.e02,
			e10 + m.e10, e11 + m.e11, e12 + m.e12,
			e20 + m.e20, e21 + m.e21, e22 + m.e22
			);
	}


	/**
	 * @brief 두 3x3 행렬의 대응하는 원소를 더합니다.
	 *
	 * @param m 행렬의 덧셈을 수행할 피연산자입니다.
	 *
	 * @return 두 행렬의 대응하는 원소를 더한 결과를 반환합니다.
	 */
	TMat3x3<T> operator+(const TMat3x3<T>& m) const
	{
		return TMat3x3<T>(
			e00 + m.e00, e01 + m.e01, e02 + m.e02,
			e10 + m.e10, e11 + m.e11, e12 + m.e12,
			e20 + m.e20, e21 + m.e21, e22 + m.e22
			);
	}


	/**
	 * @brief 두 3x3 행렬의 대응하는 원소를 뺍니다.
	 *
	 * @param m 행렬의 뺄셈을 수행할 피연산자입니다.
	 *
	 * @return 두 행렬의 대응하는 원소를 뺀 결과를 반환합니다.
	 */
	TMat3x3<T> operator-(TMat3x3<T>&& m) const
	{
		return TMat3x3<T>(
			e00 - m.e00, e01 - m.e01, e02 - m.e02,
			e10 - m.e10, e11 - m.e11, e12 - m.e12,
			e20 - m.e20, e21 - m.e21, e22 - m.e22
			);
	}


	/**
	 * @brief 두 3x3 행렬의 대응하는 원소를 뺍니다.
	 *
	 * @param m 행렬의 뺄셈을 수행할 피연산자입니다.
	 *
	 * @return 두 행렬의 대응하는 원소를 뺀 결과를 반환합니다.
	 */
	TMat3x3<T> operator-(const TMat3x3<T>& m) const
	{
		return TMat3x3<T>(
			e00 - m.e00, e01 - m.e01, e02 - m.e02,
			e10 - m.e10, e11 - m.e11, e12 - m.e12,
			e20 - m.e20, e21 - m.e21, e22 - m.e22
			);
	}


	/**
	 * @brief 두 3x3 행렬을 곱합니다.
	 *
	 * @param m 행렬의 곱셈을 수행할 피연산자입니다.
	 *
	 * @return 두 행렬을 곱한 결과를 반환합니다.
	 */
	TMat3x3<T> operator*(TMat3x3<T>&& m) const
	{
		return TMat3x3(
			e00 * m.e00 + e01 * m.e10 + e02 * m.e20,
			e00 * m.e01 + e01 * m.e11 + e02 * m.e21,
			e00 * m.e02 + e01 * m.e12 + e02 * m.e22,
			e10 * m.e00 + e11 * m.e10 + e12 * m.e20,
			e10 * m.e01 + e11 * m.e11 + e12 * m.e21,
			e10 * m.e02 + e11 * m.e12 + e12 * m.e22,
			e20 * m.e00 + e21 * m.e10 + e22 * m.e20,
			e20 * m.e01 + e21 * m.e11 + e22 * m.e21,
			e20 * m.e02 + e21 * m.e12 + e22 * m.e22
		);
	}


	/**
	 * @brief 두 3x3 행렬을 곱합니다.
	 *
	 * @param m 행렬의 곱셈을 수행할 피연산자입니다.
	 *
	 * @return 두 행렬을 곱한 결과를 반환합니다.
	 */
	TMat3x3<T> operator*(const TMat3x3<T>& m) const
	{
		return TMat3x3(
			e00 * m.e00 + e01 * m.e10 + e02 * m.e20,
			e00 * m.e01 + e01 * m.e11 + e02 * m.e21,
			e00 * m.e02 + e01 * m.e12 + e02 * m.e22,
			e10 * m.e00 + e11 * m.e10 + e12 * m.e20,
			e10 * m.e01 + e11 * m.e11 + e12 * m.e21,
			e10 * m.e02 + e11 * m.e12 + e12 * m.e22,
			e20 * m.e00 + e21 * m.e10 + e22 * m.e20,
			e20 * m.e01 + e21 * m.e11 + e22 * m.e21,
			e20 * m.e02 + e21 * m.e12 + e22 * m.e22
		);
	}


	/**
	 * @brief 두 3x3 행렬에 대응하는 원소를 더합니다.
	 *
	 * @param m 연산을 수행할 피연산자입니다.
	 *
	 * @return 연산을 수행한 행렬의 참조자를 반환합니다.
	 */
	TMat3x3<T>& operator+=(TMat3x3<T>&& m) noexcept
	{
		e00 += m.e00; e01 += m.e01; e02 += m.e02;
		e10 += m.e10; e11 += m.e11; e12 += m.e12;
		e20 += m.e20; e21 += m.e21; e22 += m.e22;

		return *this;
	}


	/**
	 * @brief 두 3x3 행렬에 대응하는 원소를 더합니다.
	 *
	 * @param m 연산을 수행할 피연산자입니다.
	 *
	 * @return 연산을 수행한 행렬의 참조자를 반환합니다.
	 */
	TMat3x3<T>& operator+=(const TMat3x3<T>& m) noexcept
	{
		e00 += m.e00; e01 += m.e01; e02 += m.e02;
		e10 += m.e10; e11 += m.e11; e12 += m.e12;
		e20 += m.e20; e21 += m.e21; e22 += m.e22;

		return *this;
	}


	/**
	 * @brief 두 3x3 행렬에 대응하는 원소를 뺍니다.
	 *
	 * @param m 연산을 수행할 피연산자입니다.
	 *
	 * @return 연산을 수행한 행렬의 참조자를 반환합니다.
	 */
	TMat3x3<T>& operator-=(TMat3x3<T>&& m) noexcept
	{
		e00 -= m.e00; e01 -= m.e01; e02 -= m.e02;
		e10 -= m.e10; e11 -= m.e11; e12 -= m.e12;
		e20 -= m.e20; e21 -= m.e21; e22 -= m.e22;

		return *this;
	}


	/**
	 * @brief 두 3x3 행렬에 대응하는 원소를 뺍니다.
	 *
	 * @param m 연산을 수행할 피연산자입니다.
	 *
	 * @return 연산을 수행한 행렬의 참조자를 반환합니다.
	 */
	TMat3x3<T>& operator-=(const TMat3x3<T>& m) noexcept
	{
		e00 -= m.e00; e01 -= m.e01; e02 -= m.e02;
		e10 -= m.e10; e11 -= m.e11; e12 -= m.e12;
		e20 -= m.e20; e21 -= m.e21; e22 -= m.e22;

		return *this;
	}


	/**
	 * @brief 3x3 행렬 원소 배열의 포인터를 얻습니다.
	 *
	 * @return 3x3 행렬 원소 배열의 포인터를 반환합니다.
	 */
	const T* GetPtr() const { return &data[0]; }


	/**
	 * @brief 3x3 행렬 원소 배열의 포인터를 얻습니다.
	 *
	 * @return 3x3 행렬 원소 배열의 포인터를 반환합니다.
	 */
	T* GetPtr() { return &data[0]; }


	/**
	 * @brief 3x3 행렬의 모든 원소가 0인 행렬을 얻습니다.
	 *
	 * @return 모든 원소가 0인 3x3 행렬을 반환합니다.
	 */
	static inline TMat3x3<T> Zero();


	/**
	 * @brief 3x3 행렬의 단위 행렬을 얻습니다.
	 *
	 * @return 3x3 행렬의 단위 행렬를 반환합니다.
	 */
	static inline TMat3x3<T> Identity();


	/**
	 * @brief 3x3 행렬의 전치 행렬을 얻습니다.
	 *
	 * @param m 원소들을 전치할 3x3 행렬입니다.
	 *
	 * @return 원소가 전치된 3x3 행렬을 반환합니다.
	 */
	static inline TMat3x3<T> Transpose(const TMat3x3<T>& m);


	/**
	 * @brief 3x3 행렬의 행렬식 값을 얻습니다.
	 *
	 * @param m 행렬식 값을 계산할 3x3 행렬입니다.
	 *
	 * @return 3x3 행렬의 행렬식 값을 반환합니다.
	 */
	static inline T Determinant(const TMat3x3<T>& m);


	/**
	 * @brief 3x3 행렬의 역행렬을 얻습니다.
	 *
	 * @param m 역행렬을 계산할 3x3 행렬입니다.
	 *
	 * @return 3x3 행렬의 역행렬을 반환합니다.
	 */
	static inline TMat3x3<T> Inverse(const TMat3x3<T>& m);


	/**
	 * @brief 3x3 행렬의 원소입니다.
	 */
	union
	{
		struct
		{
			T e00; T e01; T e02;
			T e10; T e11; T e12;
			T e20; T e21; T e22;
		};
		T data[9];
	};
};


/**
 * @brief int32_t 타입의 3x3 행렬입니다.
 */
using Mat3x3i = TMat3x3<int32_t>;


/**
 * @brief float 타입의 3x3 행렬입니다.
 */
using Mat3x3f = TMat3x3<float>;


#include "Mat3x3.inl"