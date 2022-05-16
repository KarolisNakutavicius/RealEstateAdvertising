import React, {useEffect, useState} from 'react'
import Advertisement from './Advertisement';
import AdvertisementService from '../../Services/AdvertisementService'

export default function MyAdvertisements() {
    
    const [ads, setAds] = useState([]);
    const [pageIndex,setPageIndex] = useState(1);
    const [pageInfo, setPageInfo] = useState({
        totalPages:0,
        pageSize:2,
        totalRecordsCount:0
    })

    const [message, setMessage] = useState("Loading...");

    useEffect(async () => {
        let pagedAds = await AdvertisementService.getMyAdvertisments(pageIndex, pageInfo.pageSize);

        if (pagedAds.items.length > 0) {
            setAds(pagedAds.items)
            setPageInfo({
                totalPages:pagedAds.totalPages,
                pageSize:pagedAds.pageSize,
                totalRecordsCount:pagedAds.totalRecordsCount})
            return;
        }

        setMessage("You don't have any advertisements posted");
    }, [pageIndex])

    function handleClickPrevious() {
        let nextValue = pageIndex - 1;
        if(nextValue  > 0){
            setPageIndex(nextValue)}
    }

    function handleClickNext() {
        let nextValue = pageIndex + 1;
        if(nextValue <= pageInfo.totalPages){
            setPageIndex(nextValue)}
    }

    return (
        <>
            {ads.length === 0 && (
                <h3>{message}</h3>
            )}
            <div className='d-flex justify-content-start flex-wrap'>
                {ads.map(ad => {
                    return <Advertisement key={ad.id} ad={ad} isPersonal={false}/>
                })}
            </div>

            <nav aria-label="Page navigation example" className="pagingBar">
                <ul className="pagination">
                    <li role="button" className="page-item"><a className="page-link" onClick={handleClickPrevious}>Previous</a></li>
                    {Array(pageInfo.totalPages).fill(null).map((value, index) =>
                        <li role="button" className={pageIndex === index +1 ? "page-item active" : "page-item" }
                            key={index+1} 
                            onClick={(e) => setPageIndex(index + 1)}>
                            <a className="page-link">{index + 1}</a>
                        </li>
                        )
                    }
                    <li role="button" className="page-item" onClick={handleClickNext}><a className="page-link" >Next</a></li>
                </ul>
            </nav>
        </>
    )
}
