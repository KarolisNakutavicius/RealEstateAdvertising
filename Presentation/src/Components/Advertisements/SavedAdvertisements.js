import React, {useEffect, useState} from 'react'
import Advertisement from './Advertisement';
import AdvertisementService from '../../Services/AdvertisementService'
import PagingBar from "../General/PagingBar";

function SavedAdvertisements(props) {
    
    const [ads, setAds] = useState([]);
    const [pageIndex,setPageIndex] = useState(1);
    const [pageInfo, setPageInfo] = useState({
        totalPages:0,
        pageSize:2,
        totalRecordsCount:0
    })

    const [message, setMessage] = useState("Loading...");

    useEffect(async () => {
        let pagedAds = await AdvertisementService.getMySavedAdvertisments(pageIndex, pageInfo.pageSize);

        if (pagedAds.items.length > 0) {
            setAds(pagedAds.items)
            setPageInfo({
                totalPages:pagedAds.totalPages,
                pageSize:pagedAds.pageSize,
                totalRecordsCount:pagedAds.totalRecordsCount})
            return;
        }

        setMessage("You don't have any advertisements saved");
    }, [pageIndex])

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

            <PagingBar totalPages={pageInfo.totalPages} pageIndex={pageIndex} setPageIndex={setPageIndex}/>
        </>
    )
}

export default SavedAdvertisements;