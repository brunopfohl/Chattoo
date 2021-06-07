export interface CalendarEvent {
    id: string,
    startsAt: Date,
    endsAt: Date,
    name: string,
    description: string,
    authorId: string,
    authorName: string,
    createdAt: Date,
    modifiedAt: Date
}