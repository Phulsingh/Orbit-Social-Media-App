# 🏠 ORBIT SOCIAL MEDIA APP

A private, secure family social networking application designed to connect family members across the world in one safe digital space.

> "Built for families. Not for the public."

---

## 🚀 Overview

FamilyNest is an invite-only platform where family members can:

- Stay connected through posts and chats
- Share memories (photos, videos, stories)
- Manage family relationships
- Communicate in real-time
- Celebrate important events together

Unlike traditional social media, FamilyNest is **completely private** — no public access, no ads, and no data selling.

---

## 🔐 Core Concept

- 👨‍👩‍👧‍👦 One App = One Family (isolated data)
- 🔑 Invite-only registration system
- 🛡️ Admin-controlled access
- 🔒 End-to-end privacy focus

---

## ✨ Features

### 🧱 Phase 1 — Foundation
- Family Creation (Admin)
- Invite-based Registration (Code/Link)
- Member Profiles
- Family Tree View
- Member Directory

---

### 📱 Phase 2 — Social Features
- Create Posts (Text + Media)
- Like & Comment System
- Stories (24-hour expiry)
- Photo Albums
- Family Announcements

---

### 💬 Phase 3 — Communication
- 1-to-1 Chat
- Group Chat
- Real-time Messaging (SignalR)
- Message History

---

### 📞 Phase 4 — Advanced Features
- Voice Calls
- Video Calls (WebRTC)
- Group Video Calls
- Live Location Sharing

---

### 📅 Phase 5 — Special Features
- Family Calendar (Birthdays, Events)
- Polls (Decision Making)
- Memory Book
- Recipe Sharing
- Emergency Alerts 🚨

---

## 🏗️ Tech Stack

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- SignalR (Real-time communication)
- WebRTC (Calls)

### Frontend
- React JS (Web)
- React Native (Mobile - Planned)

### Storage
- Cloudinary (Media storage)
- Azure Blob Storage (Alternative)

### Notifications
- Firebase FCM
- SignalR (In-app notifications)

---

## 🗄️ Database Design (High Level)


---

## 🧾 Key Tables

- Families
- FamilyMembers
- InviteCodes
- Posts
- Stories
- Messages
- FamilyEvents

---

## 🔐 Security Features

- Invite-only system (No public signup)
- Admin-controlled member access
- Private media storage
- No ads / No data selling
- GDPR compliant design
- Secure JWT authentication
- End-to-end encrypted chat (planned)

---

## 🧑‍💼 Roles

### 👑 Admin (Family Head)
- Invite/remove members
- Manage groups
- Control permissions

### 👤 Members
- Participate in posts & chats
- Cannot invite outsiders

---

## 📄 Pages / Modules

1. Login / Register
2. Create Family
3. Join Family
4. Home Feed
5. Members List
6. Chat List
7. Notifications
8. Profile
9. Calendar
10. Personal Chat
11. Video Call Screen
12. Admin Panel

---

## ⚙️ Project Setup

### Prerequisites

- .NET 8 SDK
- SQL Server
- Visual Studio / VS Code
- Node.js (for frontend)

---

### Backend Setup

```bash
git clone https://github.com/Phulsingh/Orbit-Social-Media-App
cd familynest

dotnet restore
dotnet build
dotnet run
