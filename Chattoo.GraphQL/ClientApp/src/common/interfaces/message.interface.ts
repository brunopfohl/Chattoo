export interface Message {
    id: string,
    content: string,
    type: string,
    userId: string,
    channelId: string,
    userName: string,
    createdAt: Date,
    modifiedAt: Date
}