template<typename T>
inline T TVec4<T>::Dot(const TVec4<T>& lhs, const TVec4<T>& rhs)
{
	return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z + lhs.w * rhs.w;
}

template<typename T>
inline T TVec4<T>::LengthSquare(const TVec4<T>& v)
{
	return v.x * v.x + v.y * v.y + v.z * v.z + v.w * v.w;
}

template<typename T>
inline float TVec4<T>::Length(const TVec4<T>& v)
{
	float lengthSquare = static_cast<float>(TVec4<T>::LengthSquare(v));
	return std::sqrtf(lengthSquare);
}

template<typename T>
inline TVec4<T> TVec4<T>::Normalize(const TVec4<T>& v)
{
	float length = TVec4<T>::Length(v);

	return TVec4<T>(
		static_cast<T>(static_cast<float>(v.x) / length),
		static_cast<T>(static_cast<float>(v.y) / length),
		static_cast<T>(static_cast<float>(v.z) / length),
		static_cast<T>(static_cast<float>(v.w) / length)
		);
}
