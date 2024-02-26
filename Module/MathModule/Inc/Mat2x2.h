#pragma once

#include <cstdint>


/**
 * @brief 템플릿 기반의 2x2 행렬입니다.
 */
template <typename T>
struct TMat2x2
{
	/**
	 * @brief 2x2 행렬의 기본 생성자입니다.
	 *
	 * @note 모든 원소의 값을 0으로 초기화합니다.
	 */
	TMat2x2() noexcept
	{
		e00 = static_cast<T>(0); e01 = static_cast<T>(0);
		e10 = static_cast<T>(0); e11 = static_cast<T>(0);
	}


	/**
	 * @brief 2x2 행렬의 생성자입니다.
	 *
	 * @param ee00 행렬의 (0, 0) 성분입니다.
	 * @param ee01 행렬의 (0, 1) 성분입니다.
	 * @param ee10 행렬의 (1, 0) 성분입니다.
	 * @param ee11 행렬의 (1, 1) 성분입니다.
	 */
	TMat2x2(
		T&& ee00, T&& ee01,
		T&& ee10, T&& ee11
	) noexcept
	{
		e00 = ee00; e01 = ee01;
		e10 = ee10; e11 = ee11;
	}


	/**
	 * @brief 2x2 행렬의 생성자입니다.
	 *
	 * @param e00 행렬의 (0, 0) 성분입니다.
	 * @param e01 행렬의 (0, 1) 성분입니다.
	 * @param e10 행렬의 (1, 0) 성분입니다.
	 * @param e11 행렬의 (1, 1) 성분입니다.
	 */
	TMat2x2(
		const T& ee00, const T& ee01,
		const T& ee10, const T& ee11
	) noexcept
	{
		e00 = ee00; e01 = ee01;
		e10 = ee10; e11 = ee11;
	}


	/**
	 * @brief 2x2 행렬의 원소를 하나의 값으로 초기화합니다.
	 *
	 * @param e 모든 원소를 초기화 할 값입니다.
	 */
	TMat2x2(T&& e) noexcept
	{
		e00 = e; e01 = e;
		e10 = e; e11 = e;
	}


	/**
	 * @brief 2x2 행렬의 원소를 하나의 값으로 초기화합니다.
	 *
	 * @param e 모든 원소를 초기화 할 값입니다.
	 */
	TMat2x2(const T& e) noexcept
	{
		e00 = e; e01 = e;
		e10 = e; e11 = e;
	}


	/**
	 * @brief 2x2 행렬의 복사 생성자입니다.
	 *
	 * @param m 원소를 복사할 행렬의 인스턴스입니다.
	 */
	TMat2x2(TMat2x2<T>&& m) noexcept
	{
		e00 = m.e00; e01 = m.e01;
		e10 = m.e10; e11 = m.e11;
	}


	/**
	 * @brief 2x2 행렬의 복사 생성자입니다.
	 *
	 * @param m 원소를 복사할 행렬의 인스턴스입니다.
	 */
	TMat2x2(const TMat2x2<T>& m) noexcept
	{
		e00 = m.e00; e01 = m.e01;
		e10 = m.e10; e11 = m.e11;
	}


	/**
	 * @brief 2x2 행렬의 대입 연산자입니다.
	 *
	 * @param m 원소를 복사할 행렬의 인스턴스입니다.
	 *
	 * @return 대입한 행렬의 참조자를 반환합니다.
	 */
	TMat2x2<T>& operator=(TMat2x2<T>&& m) noexcept
	{
		if (this == &m) return *this;

		e00 = m.e00; e01 = m.e01;
		e10 = m.e10; e11 = m.e11;

		return *this;
	}


	/**
	 * @brief 2x2 행렬의 대입 연산자입니다.
	 *
	 * @param m 원소를 복사할 행렬의 인스턴스입니다.
	 *
	 * @return 대입한 행렬의 참조자를 반환합니다.
	 */
	TMat2x2<T>& operator=(const TMat2x2<T>& m) noexcept
	{
		if (this == &m) return *this;

		e00 = m.e00; e01 = m.e01;
		e10 = m.e10; e11 = m.e11;

		return *this;
	}


	/**
	 * @brief 2x2 행렬의 모든 원소에 - 부호를 취합니다.
	 *
	 * @return 모든 원소에 -부호를 취한 새로운 행렬을 반환합니다.
	 */
	TMat2x2<T> operator-() const
	{
		return TMat2x2<T>(
			-e00, -e01,
			-e10, -e11
			);
	}


	/**
	 * @brief 두 2x2 행렬의 대응하는 원소를 더합니다.
	 *
	 * @param m 행렬의 덧셈을 수행할 피연산자입니다.
	 *
	 * @return 두 행렬의 대응하는 원소를 더한 결과를 반환합니다.
	 */
	TMat2x2<T> operator+(TMat2x2<T>&& m) const
	{
		return TMat2x2<T>(
			e00 + m.e00, e01 + m.e01,
			e10 + m.e10, e11 + m.e11
			);
	}


	/**
	 * @brief 두 2x2 행렬의 대응하는 원소를 더합니다.
	 *
	 * @param m 행렬의 덧셈을 수행할 피연산자입니다.
	 *
	 * @return 두 행렬의 대응하는 원소를 더한 결과를 반환합니다.
	 */
	TMat2x2<T> operator+(const TMat2x2<T>& m) const
	{
		return TMat2x2<T>(
			e00 + m.e00, e01 + m.e01,
			e10 + m.e10, e11 + m.e11
			);
	}


	/**
	 * @brief 두 2x2 행렬의 대응하는 원소를 뺍니다.
	 *
	 * @param m 행렬의 뺄셈을 수행할 피연산자입니다.
	 *
	 * @return 두 행렬의 대응하는 원소를 뺀 결과를 반환합니다.
	 */
	TMat2x2<T> operator-(TMat2x2<T>&& m) const
	{
		return TMat2x2<T>(
			e00 - m.e00, e01 - m.e01,
			e10 - m.e10, e11 - m.e11
			);
	}


	/**
	 * @brief 두 2x2 행렬의 대응하는 원소를 뺍니다.
	 *
	 * @param m 행렬의 뺄셈을 수행할 피연산자입니다.
	 *
	 * @return 두 행렬의 대응하는 원소를 뺀 결과를 반환합니다.
	 */
	TMat2x2<T> operator-(const TMat2x2<T>& m) const
	{
		return TMat2x2<T>(
			e00 - m.e00, e01 - m.e01,
			e10 - m.e10, e11 - m.e11
			);
	}


	/**
	 * @brief 두 2x2 행렬을 곱합니다.
	 *
	 * @param m 행렬의 곱셈을 수행할 피연산자입니다.
	 *
	 * @return 두 행렬을 곱한 결과를 반환합니다.
	 */
	TMat2x2<T> operator*(TMat2x2<T>&& m) const
	{
		return TMat2x2(
			e00 * m.e00 + e01 * m.e10,
			e00 * m.e01 + e01 * m.e11,
			e10 * m.e00 + e11 * m.e10,
			e10 * m.e01 + e11 * m.e11
		);
	}


	/**
	 * @brief 두 2x2 행렬을 곱합니다.
	 *
	 * @param m 행렬의 곱셈을 수행할 피연산자입니다.
	 *
	 * @return 두 행렬을 곱한 결과를 반환합니다.
	 */
	TMat2x2<T> operator*(const TMat2x2<T>& m) const
	{
		return TMat2x2(
			e00 * m.e00 + e01 * m.e10,
			e00 * m.e01 + e01 * m.e11,
			e10 * m.e00 + e11 * m.e10,
			e10 * m.e01 + e11 * m.e11
		);
	}


	/**
	 * @brief 두 2x2 행렬에 대응하는 원소를 더합니다.
	 *
	 * @param m 연산을 수행할 피연산자입니다.
	 *
	 * @return 연산을 수행한 행렬의 참조자를 반환합니다.
	 */
	TMat2x2<T>& operator+=(TMat2x2<T>&& m) noexcept
	{
		e00 += m.e00; e01 += m.e01;
		e10 += m.e10; e11 += m.e11;

		return *this;
	}


	/**
	 * @brief 두 2x2 행렬에 대응하는 원소를 더합니다.
	 *
	 * @param m 연산을 수행할 피연산자입니다.
	 *
	 * @return 연산을 수행한 행렬의 참조자를 반환합니다.
	 */
	TMat2x2<T>& operator+=(const TMat2x2<T>& m) noexcept
	{
		e00 += m.e00; e01 += m.e01;
		e10 += m.e10; e11 += m.e11;

		return *this;
	}


	/**
	 * @brief 두 2x2 행렬에 대응하는 원소를 뺍니다.
	 *
	 * @param m 연산을 수행할 피연산자입니다.
	 *
	 * @return 연산을 수행한 행렬의 참조자를 반환합니다.
	 */
	TMat2x2<T>& operator-=(TMat2x2<T>&& m) noexcept
	{
		e00 -= m.e00; e01 -= m.e01;
		e10 -= m.e10; e11 -= m.e11;

		return *this;
	}


	/**
	 * @brief 두 2x2 행렬에 대응하는 원소를 뺍니다.
	 *
	 * @param m 연산을 수행할 피연산자입니다.
	 *
	 * @return 연산을 수행한 행렬의 참조자를 반환합니다.
	 */
	TMat2x2<T>& operator-=(const TMat2x2<T>& m) noexcept
	{
		e00 -= m.e00; e01 -= m.e01;
		e10 -= m.e10; e11 -= m.e11;

		return *this;
	}


	/**
	 * @brief 2x2 행렬 원소 배열의 포인터를 얻습니다.
	 *
	 * @return 2x2 행렬 원소 배열의 포인터를 반환합니다.
	 */
	const T* GetPtr() const { return &data[0]; }


	/**
	 * @brief 2x2 행렬 원소 배열의 포인터를 얻습니다.
	 *
	 * @return 2x2 행렬 원소 배열의 포인터를 반환합니다.
	 */
	T* GetPtr() { return &data[0]; }


	/**
	 * @brief 2x2 행렬의 모든 원소가 0인 행렬을 얻습니다.
	 * 
	 * @return 모든 원소가 0인 2x2 행렬을 반환합니다.
	 */
	static inline TMat2x2<T> Zero();


	/**
	 * @brief 2x2 행렬의 단위 행렬을 얻습니다.
	 *
	 * @return 2x2 행렬의 단위 행렬를 반환합니다.
	 */
	static inline TMat2x2<T> Identity();


	/**
	 * @brief 2x2 행렬의 전치 행렬을 얻습니다.
	 * 
	 * @param m 원소들을 전치할 2x2 행렬입니다.
	 * 
	 * @return 원소가 전치된 2x2 행렬을 반환합니다.
	 */
	static inline TMat2x2<T> Transpose(const TMat2x2<T>& m);


	/**
	 * @brief 2x2 행렬의 행렬식 값을 얻습니다.
	 * 
	 * @param m 행렬식 값을 계산할 2x2 행렬입니다.
	 * 
	 * @return 2x2 행렬의 행렬식 값을 반환합니다.
	 */
	static inline T Determinant(const TMat2x2<T>& m);


	/**
	 * @brief 2x2 행렬의 역행렬을 얻습니다.
	 * 
	 * @param m 역행렬을 계산할 2x2 행렬입니다.
	 * 
	 * @return 2x2 행렬의 역행렬을 반환합니다.
	 */
	static inline TMat2x2<T> Inverse(const TMat2x2<T>& m);

	
	/**
	 * @brief 2x2 행렬의 원소입니다.
	 */
	union
	{
		struct
		{
			T e00; T e01;
			T e10; T e11;
		};
		T data[4];
	};
};


/**
 * @brief int32_t 타입의 2x2 행렬입니다.
 */
using Mat2x2i = TMat2x2<int32_t>;


/**
 * @brief float 타입의 2x2 행렬입니다.
 */
using Mat2x2f = TMat2x2<float>;


#include "Mat2x2.inl"