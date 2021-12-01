import React from 'react'

export interface MessageComponentProps {
    isFromCurrentUser: boolean,
    isStartOfBatch: boolean,
    isEndOfBatch: boolean,
    content: string,
    userName: string,
    createdAt: Date
};

const MessageComponent: React.FC<any> = (props: MessageComponentProps) => {
    const { isFromCurrentUser, isStartOfBatch, isEndOfBatch, content, userName, createdAt } = props;

    const showTime = isStartOfBatch;

    return (
        <>
            {showTime && <span>{new Date(createdAt).toUTCString()}</span> }
            <div {...{isFromCurrentUser}}>
                <div {...{isFromCurrentUser}}>
                    {isStartOfBatch && userName}
                    <span {...{isFromCurrentUser, isStartOfBatch, isEndOfBatch}}>
                        {content}
                    </span>
                </div>
            </div>
        </>
    );
}

export default MessageComponent;