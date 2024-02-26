template<typename T>
inline T TVec2<T>::Dot(const TVec2<T>& lhs, const TVec2<T>& rhs)
{
	return lhs.x * rhs.x + lhs.y * rhs.y;
}

template<typename T>
inline T TVec2<T>::Cross(const TVec2<T>& lhs, const TVec2<T>& rhs)
{
	return lhs.x * rhs.y - lhs.y * rhs.x;
}

template<typename T>
inline T TVec2<T>::LengthSquare(const TVec2<T>& v)
{
	return v.x * v.x + v.y * v.y;
}

template<typename T>
inline float TVec2<T>::Length(const TVec2<T>& v)
{
	float lengthSquare = static_cast<float>(TVec2<T>::LengthSquare(v));
	return std::sqrtf(lengthSquare);
}

template<typename T>
inline TVec2<T> TVec2<T>::Normalize(const TVec2<T>& v)
{
	float length = TVec2<T>::Length(v);

	return TVec2<T>(
		static_cast<T>(static_cast<float>(v.x) / length),
		static_cast<T>(static_cast<float>(v.y) / length)
		);
}
