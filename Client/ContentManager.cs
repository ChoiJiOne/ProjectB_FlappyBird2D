using System;
using System.Collections.Generic;


/**
 * @brief 게임 내의 컨텐츠를 생성하고 관리하는 싱글턴 클래스입니다.
 */
class ContentManager
{
    /**
     * @brief 게임 내의 컨텐츠를 관리하는 매니저의 인스턴스를 얻습니다.
     * 
     * @return 게임 내의 컨텐츠를 관리하는 매니저의 인스턴스를 반환합니다.
     */
    public static ContentManager Get()
    {
        if(contentManager_ == null)
        {
            contentManager_ = new ContentManager();
        }

        return contentManager_;
    }


    /**
     * @brief 게임 내의 컨텐츠를 관리하는 매니저를 초기화합니다.
     * 
     * @param contentPath 게임 내의 컨텐츠가 있는 최상위 경로입니다.
     */
    public void Setup(string contentPath)
    {
        if (bIsSetup_) return;

        contentPath_ = contentPath;

        bIsSetup_ = true;
    }


    /**
     * @brief 게임 내의 컨텐츠 리소스들을 명시적으로 정리합니다.
     */
    public void Cleanup()
    {
        if (!bIsSetup_) return;

        foreach(KeyValuePair<string, IContent> contentKeyValue in contents_)
        {
            IContent content = contentKeyValue.Value;
            content.Release();
        }

        bIsSetup_ = false;
    }


    /**
     * @brief 시그니처 값에 대응하는 리소스가 있는지 확인합니다.
     * 
     * @param signature 리소스가 있는지 확인할 시그니처 값입니다.
     * 
     * @return 시그니처 값에 대응하는 리소스가 있다면 true, 그렇지 않다면 false를 반환합니다.
     */
    public bool IsValid(string signature)
    {
        return contents_.ContainsKey(signature);
    }


    /**
     * @brief 텍스처 리소스를 생성합니다.
     * 
     * @note 이때, 텍스처 리소스의 경로는 Content 폴더 기준입니다.
     * 
     * @param signature 다른 텍스처 리소스와 구분하기 위한 시그니처 값입니다.
     * @param path 텍스처 리소스의 경로입니다. 
     * 
     * @throws 
     * - 시그니처 값이 이미 존재하면 예외를 던집니다.
     * - 텍스처 리소스 생성에 실패하면 예외를 던집니다.
     */
    public Texture CreateTexture(string signature, string path)
    {
        if(IsValid(signature))
        {
            throw new Exception("collision texture resource signature...");
        }

        Texture texture = new Texture(contentPath_ + path);
        contents_.Add(signature, texture);

        return texture;
    }


    /**
     * @brief 텍스처 리소스를 얻습니다.
     * 
     * @param signature 텍스처 리소스에 대응하는 시그니처 값입니다.
     * 
     * @throws 
     * 시그니처 값에 대응하는 텍스처 리소스가 존재하지 않으면 예외를 던집니다.
     * 시그니처 값에 대응하는 컨텐츠가 텍스처가 아니면 예외를 던집니다.
     * 
     * @return 시그니처 값에 대응하는 텍스처 리소스입니다.
     */
    public Texture GetTexture(string signature)
    {
        if(!IsValid(signature))
        {
            throw new Exception("can't find texture resource from signature...");
        }

        IContent content = contents_[signature];
        if(!(content is Texture))
        {
            throw new Exception("signature isn't texture resource...");
        }

        return (content as Texture);
    }


    /**
     * @brief 트루 타입 폰트 리소스를 생성합니다.
     * 
     * @note 이때, 트루 타입 폰트 리소스의 경로는 Content 폴더 기준입니다.
     * 
     * @param path 트루 타입 폰트 리소스의 경로입니다.
     * @param beginCodePoint 텍스처 아틀라스에 표시할 시작 문자입니다.
     * @param endCodePoint 텍스처 아틀라스에 표시할 끝 문자입니다.
     * @param size 폰트의 크기입니다.
     * 
     * @throws
     * - 시그니처 값이 이미 존재하면 예외를 던집니다.
     * - 트루 타입 폰트 리소스 생성에 실패하면 예외를 던집니다.
     */
    public TTFont CreateTTFont(string signature, string path, ushort beginCodePoint, ushort endCodePoint, int size)
    {
        if (IsValid(signature))
        {
            throw new Exception("collision true type font resource signature...");
        }

        beginCodePoint = Math.Max(beginCodePoint, (ushort)0);
        endCodePoint = Math.Max(endCodePoint, (ushort)0);

        beginCodePoint = Math.Min(beginCodePoint, endCodePoint);
        endCodePoint = Math.Max(beginCodePoint, endCodePoint);

        TTFont font = new TTFont(contentPath_ + path, beginCodePoint, endCodePoint, size);
        contents_.Add(signature, font);

        return font;
    }


    /**
     * @brief 트루 타입 폰트 리소스를 얻습니다.
     * 
     * @param signature 트루 타입 폰트 리소스에 대응하는 시그니처 값입니다.
     * 
     * @throws 
     * 시그니처 값에 대응하는 트루 타입 폰트 리소스가 존재하지 않으면 예외를 던집니다.
     * 시그니처 값에 대응하는 컨텐츠가 트루 타입 폰트가 아니면 예외를 던집니다.
     * 
     * @return 시그니처 값에 대응하는 텍스처 리소스입니다.
     */
    public TTFont GetTTFont(string signature)
    {
        if (!IsValid(signature))
        {
            throw new Exception("can't find true type font resource from signature...");
        }

        IContent content = contents_[signature];
        if (!(content is TTFont))
        {
            throw new Exception("signature isn't true type font resource...");
        }

        return (content as TTFont);
    }


    /**
     * @brief 게임 컨텐츠를 삭제합니다.
     * 
     * @note 시그니처 값에 대응하는 게임 컨텐츠 리소스가 존재하지 않으면 아무 동작도 수행하지 않습니다.
     * 
     * @param signature 게임 컨텐츠 리소스에 대응하는 시그니처 값입니다.
     */
    public void RemoveContent(string signature)
    {
        if (!IsValid(signature)) return;

        IContent content = contents_[signature];
        contents_.Remove(signature);
    }


    /**
     * @brief 생성자는 외부에서 호출할 수 없도록 감춤니다.
     */
    private ContentManager() { }


    /**
     * @brief 게임 컨텐츠들이 저장된 최상위 경로입니다.
     */
    private string contentPath_;


    /**
     * @brief 컨텐츠 매니저가 초기화된 적이 있는지 확인합니다.
     */
    private bool bIsSetup_ = false;


    /**
     * @brief 매니저가 관리하는 게임 내의 컨텐츠입니다.
     */
    private Dictionary<string, IContent> contents_ = new Dictionary<string, IContent>();


    /**
     * @brief 컨텐츠를 관리하는 컨텐츠 매니저의 인스턴스입니다.
     */
    private static ContentManager contentManager_;
}