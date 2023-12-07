# 💡 [프로젝트 소개]
**Title : 십(장생)과 함께** <br/>
**장르 : Survivors-Like, Rogue-Like <br/>
**플랫폼 : PC <br/>
**개발엔진 : Unity3D 2022.3.2f1  <br/>
**시놉시스 : 적어주셈 .

---

# ⏰ [개발 기간]
2023.11.30 - 2023.12.07

---

# 👥 [멤버 구성]
성연호 - Scene, UI, 스테이터스 강화 기능            <br/>
강건욱 - 몬스터 클래스, 몬스터 스폰 기능            <br/>
우진영 - 게임매니저, 데이터매니저, 아이템 드랍            <br/>
박준욱 - 아이템 클래스, 밸런스            <br/>
최성재 - 플레이어 클래스, 오디오 매니저            <br/>

---

# ▶️ [플레이 영상]
링크 주셈.


---

# ⚙ [주요 기능]

## 1. 게임 시작 화면
![image](https://github.com/chamhok/10JO_Roguelike_GAME/assets/148977728/7a057a7f-f904-4b52-8fbd-a87771963e2d) <br/>
타이틀 화면입니다. <br/>
`탈출 시도` 버튼을 누르면 게임이 시작되며 스테이지 1이 시작됩니다. <br/>
`영혼 강화` 버튼을 누르면 캐릭터 스탯 강화 화면으로 넘어갑니다. <br/>

## 2. 캐릭터 스탯 강화 화면
![image](https://github.com/chamhok/10JO_Roguelike_GAME/assets/148977728/5ce236b2-cb77-444d-a87e-8bf561332ad7) <br/>
재화를 소모하여 캐릭터 스탯을 영구적으로 강화할 수 있습니다. 강화 정보는 저장되며, 게임을 껐다 켜도 유지됩니다. <br/>
재화는 스테이지를 진행하며 획득합니다. <br/>

## 3. 스토리 연출
![image](https://github.com/chamhok/10JO_Roguelike_GAME/assets/148977728/63e00957-c292-400c-8278-24b47f424de6) <br/>
각 스테이지로 진입하기 전에, 현재 진행 상황에 따른 스토리 연출을 보여줍니다. <br/>
스페이스 바로 연출을 스킵할 수 있습니다. <br/>

## 4. 스테이지 화면
![image](https://github.com/chamhok/10JO_Roguelike_GAME/assets/148977728/ce7c2b3d-5fe6-4a26-89b0-2a235f2c1c95)
스테이지 씬에 입장하면, 현재 스테이지의 이름을 보여주고 게임이 시작됩니다. <br/>
상단 바는 플레이어의 레벨과 현재 경험치 획득 정보를 보여줍니다. <br/>
중상단에서는 현재 스테이지의 보스 등장까지의 남은 시간을 보여줍니다. <br/>
좌상단 아이콘은 현재 가지고 있는 아이템의 정보를 보여줍니다. <br/>
우상단에서는 획득한 재화의 수를 보여줍니다. <br/>

### 게임 매니저/데이터 매니저
스테이지 씬이 로드되면, 게임 매니저는 스테이지 생성에 필요한 정보를 불러오고, 스테이지를 초기화합니다. <br/>
데이터 매니저는 씬 전환 시 유지되어야 하는 데이터를 취합하여, 다음 씬 로드 시 취합한 데이터를 이용하여 씬을 초기화합니다. <br/>

### 플레이어
![image](https://github.com/chamhok/10JO_Roguelike_GAME/assets/148977728/67246bb3-b629-4618-a046-9e995045f983) <br/>
플레이어 캐릭터는 WASD 키로 이동합니다. <br/>
머리 위에는 현재 체력을 나타내는 체력바가 존재합니다. <br/>

### 십장생 아이템
![image](https://github.com/chamhok/10JO_Roguelike_GAME/assets/148977728/c5c0327b-3ae9-4094-8ca6-b041f7a8c0d5) <br/>
10가지의 십장생 아이템이 존재합니다. 십장생 아이템 별 기능은 다음과 같습니다. <br/>

| 이름 | 기능 |
|------|------|
|돌    |기본 무기입니다. 마우스 커서 방향으로 1~3개의 돌을 던집니다.|
|달    |캐릭터 주변을 공전하며, 닿은 몬스터에게 피해를 줍니다.|
|해    |일정 시간마다 아주 넓은 범위 피해를 입힙니다.|
|소나무|마우스 커서 방향으로 솔방울을 던집니다. 솔방울은 어딘가에 닿으면 폭발합니다.|
|두루미|캐릭터의 이동속도를 증가시킵니다.|
|거북이|피격을 1회 방어해주는 보호막을 생성합니다. 보호막은 중첩 가능합니다.|
|사슴  |마우스 커서 방향으로 뿔을 부채꼴 형태로 발사합니다.|
|물    |마우스 커서 방향으로 물대포를 발사합니다.|
|불로초|체력을 완전 회복합니다.|
|산    |돌, 소나무, 불로초를 획득합니다.|

### 몬스터/스포너
![img (2)](https://github.com/chamhok/10JO_Roguelike_GAME/assets/148977728/43ccdcb8-bc6d-4296-b273-f3ca397c8214) <br/>
![image](https://github.com/chamhok/10JO_Roguelike_GAME/assets/148977728/d65bf3a0-e59d-42c2-917f-ce57adb6a8fb) <br/>
스테이지별로 일반 몬스터, 원거리 공격 몬스터, 보스 몬스터가 존재합니다. <br/>
몬스터 객체들은 오브젝트풀링으로 관리하고 있습니다. <br/>
몬스터 스포너는 스테이지 별 스폰 데이터를 입력받아 몬스터를 스폰합니다. <br/>
몬스터 사망 시, 경험치와 돈을 드랍합니다. <br/>
보스 몬스터 사망 시, 스테이지가 클리어됩니다. <br/>

### 경험치, 돈
경험치와 돈 오브젝트는 드랍 할때 자연스러운 연출을 위해 베지에 곡선을 이용했습니다. <br/>

### 아이템 선택 창
![image](https://github.com/chamhok/10JO_Roguelike_GAME/assets/148977728/eebe8926-dbf5-4d2e-852c-a186a8f94b50) <br/>
플레이어가 레벨 업을 하면, 아이템 선택 창이 열리고 게임 진행을 일시 정지합니다. <br/>
플레이어는 창이 열려있는 동안, 충분히 고민하고 원하는 십장생 아이템을 선택할 수 있습니다. <br/>
아이템 선택이 끝나면 게임을 재개합니다. <br/>

---

